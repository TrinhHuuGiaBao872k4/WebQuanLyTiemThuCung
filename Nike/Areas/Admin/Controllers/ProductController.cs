using Nike.DesignPatterm.CommandPatterm;
using Nike.Models;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly QuanLySanPhamEntities _db = new QuanLySanPhamEntities();

        public ActionResult Index(string sort, int? page, string searchString)
        {
            const int pageSize = 10;
            int pageNumber = page ?? 1;

            var products = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                products = products.Where(p =>
                    p.ProductName.ToLower().Contains(searchString) ||
                    p.Catalog.CatalogName.ToLower().Contains(searchString));
            }
            else if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "pre-sold":
                        products = products.Where(p => p.SoLuong < 50 && p.SoLuong > 0);
                        break;
                    case "sold":
                        products = products.Where(p => p.SoLuong == 0);
                        break;
                    case "now":
                        products = products.OrderByDescending(p => p.NgayNhapHang);
                        break;
                }
            }

            ViewBag.Sort = sort;
            ViewBag.searchStr = searchString;
            ViewBag.totalPage = Math.Ceiling((double)products.Count() / pageSize);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.CatalogId = new SelectList(_db.Catalogs, "ID", "CatalogName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, Product model)
        {
            if (ModelState.IsValid)
            {
                model.Picture = HandleFileUpload(file, "/Hinh/Product/hinhphone.jpg");

                // Sử dụng Command Pattern
                var command = new CreateProductCommand(model, p =>
                {
                    _db.Products.Add(p);
                    _db.SaveChanges();
                });

                command.Execute();

                return RedirectToAction("Index");
            }

            ViewBag.CatalogId = new SelectList(_db.Catalogs, "ID", "CatalogName", model.CatalogId);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return HttpNotFound();

            ViewBag.CatalogId = new SelectList(_db.Catalogs, "ID", "CatalogName", product.CatalogId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _db.Products.Find(model.Id);
                if (existingProduct == null) return HttpNotFound();

                existingProduct.Picture = HandleFileUpload(file, existingProduct.Picture);

                // Sử dụng Command Pattern
                var command = new UpdateProductCommand(
                    existingProduct,
                    model,
                    p =>
                    {
                        _db.Entry(p).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                );

                command.Execute();

                return RedirectToAction("Index");
            }

            ViewBag.CatalogId = new SelectList(_db.Catalogs, "ID", "CatalogName", model.CatalogId);
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return HttpNotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return HttpNotFound();

            // Sử dụng Command Pattern
            var command = new DeleteProductCommand(product, p =>
            {
                _db.Products.Remove(p);
                _db.SaveChanges();
            });

            try
            {
                command.Execute();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("Không xóa được do có liên quan đến bản khác!");
            }
        }

        private string HandleFileUpload(HttpPostedFileBase file, string defaultPath)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Hinh"), fileName);
                file.SaveAs(path);
                return fileName;
            }
            return defaultPath;
        }
    }
}