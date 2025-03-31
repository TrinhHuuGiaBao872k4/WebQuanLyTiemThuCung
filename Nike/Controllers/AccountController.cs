using Nike.DesignPattern.StrategyPattern;
using Nike.Models;
 // Import namespace chứa các chiến lược
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nike.DesignPatterns.Proxy;

namespace Nike.Controllers
{
    public class AccountController : Controller
    {
        private QuanLySanPhamEntities _db = new QuanLySanPhamEntities();

        // Hàm hỗ trợ để lấy chiến lược dựa trên trạng thái
        private IOrderStatusStrategy GetOrderStatusStrategy(string status)
        {
            switch (status)
            {
                case "Wait":
                    return new WaitOrderStrategy();
                case "Deli":
                    return new DeliOrderStrategy();
                case "Done":
                    return new DoneOrderStrategy();
                case "Cancel":
                    return new CancelOrderStrategy();
                default:
                    throw new ArgumentException("Trạng thái không hợp lệ");
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang khachhang)
        {
            if (ModelState.IsValid)
            {
                var check = _db.KhachHangs.FirstOrDefault(s => s.Email == khachhang.Email);
                if (check == null)
                {
                    String anh = "user.jpg";
                    khachhang.Picture = anh;
                    _db.KhachHangs.Add(khachhang);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                KhachHang kh = _db.KhachHangs.FirstOrDefault(s => s.Email.Equals(email) && s.Password.Equals(password));
                if (kh != null)
                {
                    Session["Taikhoan"] = kh;
                    Session["NV"] = null;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Vui lòng thử lại, xin cảm ơn.";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ProFile()
        {
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            KhachHang khachHang = _db.KhachHangs.Find(kh.idUser);

            if (kh != null && khachHang != null)
            {
                return View(khachHang);
            }
            return HttpNotFound();
        }

        public ActionResult EditProFile(int idUser)
        {
            KhachHang khsession = (KhachHang)Session["Taikhoan"];
            KhachHang kh = _db.KhachHangs.Find(idUser);
            kh.ConfirmPassword = kh.Password;
            if (khsession.idUser != kh.idUser)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProFile([Bind(Include = "idUser,FirstName,LastName,Email,Picture,Address,NgaySinh,CMT,Sdt,Password,ConfirmPassword")] KhachHang kh, HttpPostedFileBase file)
        {
            KhachHang khachhang = _db.KhachHangs.Find(kh.idUser);
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    String path = System.IO.Path.Combine(Server.MapPath("~/Hinh/KhachHang"), pic);
                    file.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    khachhang.Picture = pic;
                }
                khachhang.FirstName = kh.FirstName;
                khachhang.LastName = kh.LastName;
                khachhang.Email = kh.Email;
                khachhang.Address = kh.Address;
                khachhang.ConfirmPassword = kh.ConfirmPassword;

                if (kh.NgaySinh != null)
                {
                    khachhang.NgaySinh = kh.NgaySinh;
                }
                if (kh.CMT != null)
                {
                    khachhang.CMT = kh.CMT;
                }

                if (kh.Sdt != null)
                {
                    khachhang.Sdt = kh.Sdt;
                }
                _db.Entry(khachhang).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("ProFile");
            }
            return View(kh);
        }

        public ActionResult ChangePassword(int idUser)
        {
            KhachHang khsession = (KhachHang)Session["Taikhoan"];
            KhachHang kh = _db.KhachHangs.Find(idUser);
            kh.ConfirmPassword = null;
            kh.Password = null;
            if (khsession.idUser != kh.idUser)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "idUser,Password,ConfirmPassword,oldPassword")] KhachHang kh, HttpPostedFileBase file)
        {
            KhachHang khachhang = _db.KhachHangs.Find(kh.idUser);
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Email");
            if (ModelState.IsValid)
            {
                if (!khachhang.Password.Equals(kh.oldPassword))
                {
                    ModelState.AddModelError(nameof(KhachHang.oldPassword), "Mật khẩu cũ không đúng");
                    return View(kh);
                }
                khachhang.Password = kh.Password;
                khachhang.ConfirmPassword = kh.ConfirmPassword;
                _db.Entry(khachhang).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("ProFile");
            }
            return View(kh);
        }

        public ActionResult OrderList(string sr)
        {
            var orderList = (from s in _db.Orders select s).ToList();
            var orderDetail = (from s in _db.Order_Detail select s).ToList();
            ViewBag.orderDetail = orderDetail;

            if (String.IsNullOrEmpty(sr))
            {
                ViewBag.orderList = orderList;
            }
            else
            {
                IOrderStatusStrategy strategy = GetOrderStatusStrategy(sr);
                ViewBag.orderList = orderList.Where(s => s.Status == strategy.GetType().Name.Replace("OrderStrategy", ""));
            }

            KhachHang kh = new KhachHang();
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                kh = (KhachHang)Session["Taikhoan"];
            }
            return View(kh);
        }

        public ActionResult CancelOrder(int ID)
        {
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            if (kh == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var orderService = new OrderServiceProxy(_db, kh);
                orderService.CancelOrder(ID);
            }
            catch (UnauthorizedAccessException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error"); // Trả về view thông báo lỗi
            }

            return RedirectToAction("OrderList");
        }

        [HttpPost, ActionName("CancelOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelOrderConfirmed(int ID)
        {
            try
            {
                Order order = _db.Orders.Find(ID);
                IOrderStatusStrategy strategy = new CancelOrderStrategy();
                strategy.ProcessOrder(order);

                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch
            {
                // Xử lý lỗi nếu cần
            }
            return RedirectToAction("OrderList");
        }

        public ActionResult HoaDon(int ID)
        {
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            if (kh == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var orderService = new OrderServiceProxy(_db, kh);
                var order = orderService.GetOrderDetails(ID);
                return View(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error"); // Trả về view thông báo lỗi
            }
        }
    }
}