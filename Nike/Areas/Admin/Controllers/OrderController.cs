using Nike.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Nike.DesignPattern.FacadePattern;
using System.Net;

namespace Nike.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly QuanLySanPhamEntities _db = new QuanLySanPhamEntities();
        private readonly OrderFacade _orderFacade;

        public OrderController()
        {
            _orderFacade = new OrderFacade(_db);
        }

        // GET: Admin/Order
        public ActionResult Index(string searchStr, string sort, int? page)
        {
            const int pageSize = 10;
            int pageNumber = page ?? 1;

            var orders = _orderFacade.GetOrders(searchStr, sort);
            ViewBag.orderList = orders;
            ViewBag.searchStr = searchStr;
            ViewBag.Sort = sort;

            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = _orderFacade.GetOrderDetail(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        public ActionResult Edit(int Id)
        {
            if (Id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = _orderFacade.GetOrderForEdit(Id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.Status = new SelectList(_db.Order_Status, "Status", "Status", order.Status);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] Order model)
        {
            try
            {
                _orderFacade.ProcessOrder(model.ID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        
        public ActionResult XoaDon(int Id) 
        {
            if (Id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var order = _orderFacade.GetOrderForDelete(Id);
            if (order == null)
            {
                return HttpNotFound();
            }
            
            return View(order);
        }

        [HttpPost, ActionName("XoaDon")]
        public ActionResult XacNhanXoaDon(int Id)
        {
            try
            {
                _orderFacade.CancelOrder(Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content($"Không xóa được: {ex.Message}");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}