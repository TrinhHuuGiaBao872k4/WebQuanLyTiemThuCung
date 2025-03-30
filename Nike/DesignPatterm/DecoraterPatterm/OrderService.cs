using Nike.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nike.Areas.Admin.Services
{
    public class OrderService : IOrderService
    {
        private readonly QuanLySanPhamEntities _db;

        public OrderService(QuanLySanPhamEntities db)
        {
            _db = db;
        }

        public List<Order> GetOrders()
        {
            return _db.Orders.ToList();
        }
    }
}