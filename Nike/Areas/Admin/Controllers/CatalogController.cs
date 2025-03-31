using Nike.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Nike.DesignPattern.RepositoryPattern;

namespace Nike.Areas.Admin.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IRepository<Catalog> _catalogRepository;

        public CatalogController()
        {
            // Initialize with concrete repository
            _catalogRepository = new CatalogRepository(new QuanLySanPhamEntities());
        }

        // GET: Admin/Catalog
        public ActionResult Index()
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.MaChucVu == 1)
            {
                return RedirectToAction("Index", "Home");
            }

            var catalogs = _catalogRepository.GetAll();
            return View(catalogs);
        }

        public ActionResult Create()
        {
            return View(new Catalog() { ID = 0, CatalogCode = "", CatalogName = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catalog model)
        {
            if (ModelState.IsValid)
            {
                // Auto-increment ID (remove if using database auto-increment)
                var allCatalogs = _catalogRepository.GetAll();
                model.ID = allCatalogs.Any() ? allCatalogs.Last().ID + 1 : 1;

                _catalogRepository.Add(model);
                _catalogRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Catalog catalog = _catalogRepository.GetById(ID.Value);
            if (catalog == null)
            {
                return HttpNotFound();
            }

            return View(catalog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CatalogCode,CatalogName")] Catalog model)
        {
            if (ModelState.IsValid)
            {
                var existingCatalog = _catalogRepository.GetById(model.ID);
                if (existingCatalog == null)
                {
                    return HttpNotFound();
                }

                // Update properties
                existingCatalog.CatalogCode = model.CatalogCode;
                existingCatalog.CatalogName = model.CatalogName;

                _catalogRepository.Update(existingCatalog);
                _catalogRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Catalog catalog = _catalogRepository.GetById(ID.Value);
            if (catalog == null)
            {
                return HttpNotFound();
            }

            return View(catalog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ID)
        {
            Catalog catalog = _catalogRepository.GetById(ID);
            if (catalog != null)
            {
                _catalogRepository.Delete(catalog);
                _catalogRepository.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}