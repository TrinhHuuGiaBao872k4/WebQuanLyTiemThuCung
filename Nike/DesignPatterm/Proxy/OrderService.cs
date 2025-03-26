using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Nike.Models;

namespace Nike.DesignPatterns.Proxy
{
    public class OrderService : IOrderService
    {
        private QuanLySanPhamEntities _db;

        public OrderService(QuanLySanPhamEntities db)
        {
            _db = db;
        }

        public void CancelOrder(int orderId)
        {
            var order = _db.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = "Cancel";
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

        public Order GetOrderDetails(int orderId)
        {
            return _db.Orders.Find(orderId);
        }
    }
}