using Nike.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Nike.DesignPatterns.Decorators
namespace Nike.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private QuanLySanPhamEntities _db = new QuanLySanPhamEntities();
        private readonly IOrderService _orderService;
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public HomeController()
        {
            _orderService = new OrderServiceLoggingDecorator(
                new OrderService(_db),
                new FileLogger());
        }

        [HttpGet]
        public ActionResult getDataOrder()
        {
            bool proxyCreation = _db.Configuration.ProxyCreationEnabled;
            _db.Configuration.ProxyCreationEnabled = false;

            try
            {
                var result = _orderService.GetOrders();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
            finally
            {
                _db.Configuration.ProxyCreationEnabled = proxyCreation;
            }
        }
        //Đăng nhập - đăng xuất admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(NhanVien objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = _db.NhanViens.Where(a => a.Email.Equals(objUser.Email) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                //var data = _db.NhanViens.Where(s => s.Email.Equals(objUser.Email) && s.Password.Equals(objUser.Password)).ToList();
                if (obj != null && obj.MaChucVu == 2)
                {
                    Session["NV"] = obj;
                    return RedirectToAction("Index", "Home");
                }
                else if (obj != null && obj.MaChucVu == 1)
                {
                    Session["NV"] = obj;
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Không phải là tài khoản quản trị viên");
                }
            }
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["NV"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        public ActionResult Logout()
        {
            Session["NV"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}