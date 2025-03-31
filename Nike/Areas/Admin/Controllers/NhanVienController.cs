using Nike.DesignPattern.TemplateMethodPattern;
using Nike.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly QuanLySanPhamEntities _db;
        private readonly NhanVienCrudTemplate _crudTemplate;

        public NhanVienController()
        {
            _db = new QuanLySanPhamEntities();
            _crudTemplate = new NhanVienCrudTemplate(_db);
        }

        public ActionResult Index(string searchString)
        {
            var nv = (NhanVien)Session["NV"];
            if (nv?.MaChucVu == 2) return RedirectToAction("Index", "Home");

            var dsNhanVien = _db.NhanViens.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                dsNhanVien = dsNhanVien.Where(s => s.FullName.ToLower().Contains(searchString));
            }

            return View(dsNhanVien.ToList());
        }

        public ActionResult Create()
        {
            PrepareViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, NhanVien nhanvien)
        {
            return _crudTemplate.ExecuteCreate(
                entity: nhanvien,
                file: file,
                beforeSave: e =>
                {
                    if (string.IsNullOrEmpty(e.Password))
                        e.Password = "default@123";
                },
                successResult: () => RedirectToAction("Index"),
                errorResult: () =>
                {
                    PrepareViewBag();
                    return View(nhanvien);
                }
            );
        }

        public ActionResult Edit(int id)
        {
            var nhanvien = _db.NhanViens.Find(id);
            if (nhanvien == null) return HttpNotFound();

            PrepareViewBag(nhanvien.MaChucVu);
            return View(nhanvien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NhanVien model, HttpPostedFileBase file)
        {
            var existing = _db.NhanViens.Find(model.Id);
            if (existing == null) return HttpNotFound();

            return _crudTemplate.ExecuteUpdate(
                entity: model,
                existing: existing,
                file: file,
                updateAction: e =>
                {
                    e.FullName = model.FullName;
                    e.Email = model.Email;
                    e.Address = model.Address;
                    e.NgaySinh = model.NgaySinh ?? e.NgaySinh;
                    e.Password = model.Password;
                    e.MaChucVu = model.MaChucVu;
                    e.Sex = model.Sex;
                    e.Sdt = model.Sdt;
                },
                successResult: () => RedirectToAction("Index"),
                errorResult: () =>
                {
                    PrepareViewBag(model.MaChucVu);
                    return View(model);
                }
            );
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nhanvien = _db.NhanViens.Find(id);
            if (nhanvien == null) return HttpNotFound();

            return View(nhanvien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var nhanvien = _db.NhanViens.Find(id);
                if (nhanvien == null) return HttpNotFound();

                _db.NhanViens.Remove(nhanvien);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _crudTemplate.LogError(ex);
                return View("Delete", _db.NhanViens.Find(id));
            }
        }

        public ActionResult Detail(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nhanvien = _db.NhanViens.Find(id);
            if (nhanvien == null) return HttpNotFound();

            return View(nhanvien);
        }

        private void PrepareViewBag(int? maChucVu = null)
        {
            ViewBag.MaChucVu = new SelectList(_db.ChucVus, "MaChucVu", "ChucVu1", maChucVu);
        }
    }
}