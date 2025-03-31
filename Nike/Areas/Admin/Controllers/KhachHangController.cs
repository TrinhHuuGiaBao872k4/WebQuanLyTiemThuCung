using Nike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nike.DesignPatterm.PrototypePatterm;
namespace Nike.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly QuanLySanPhamEntities _db = new QuanLySanPhamEntities();

        public KhachHangController()
        {
            // Initialize default prototypes (could also be done in Application_Start)
            if (!PrototypeManager.HasPrototype("default"))
            {
                PrototypeManager.RegisterPrototype("default", new KhachHang
                {
                    HoTen = "Khách hàng mới",
                    Email = "template@email.com",
                    DiemTichLuy = 0,
                    GioiTinh = true,
                    NgaySinh = DateTime.Now.AddYears(-20)
                });
            }
        }

        public ActionResult Index(string searchStr)
        {
            // Check employee permission
            var nv = (NhanVien)Session["NV"];
            if (nv?.MaChucVu == 1)
            {
                return RedirectToAction("Index", "Order");
            }

            var dsKhachHang = GetFilteredCustomers(searchStr);
            ViewBag.dsKH = dsKhachHang;

            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KhachHang khachhang = _db.KhachHangs.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }

            return View(khachhang);
        }

        public ActionResult CreateFromTemplate(string prototypeKey)
        {
            try
            {
                var newCustomer = PrototypeManager.GetClone(prototypeKey);
                newCustomer.Email = GenerateUniqueEmail(newCustomer.Email);
                return View("Create", newCustomer);
            }
            catch (KeyNotFoundException)
            {
                return HttpNotFound($"Prototype '{prototypeKey}' not found");
            }
        }

        private List<KhachHang> GetFilteredCustomers(string searchStr)
        {
            var query = _db.KhachHangs.AsQueryable();

            if (!string.IsNullOrEmpty(searchStr))
            {
                searchStr = searchStr.ToLower();
                query = query.Where(s => s.Email != null && s.Email.ToLower().Contains(searchStr));
            }

            return query.ToList();
        }

        private string GenerateUniqueEmail(string baseEmail)
        {
            return $"{Guid.NewGuid().ToString("N").Substring(0, 8)}_{baseEmail}";
        }
        // hell
    }

}