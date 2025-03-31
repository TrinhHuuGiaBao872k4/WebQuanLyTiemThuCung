using Nike.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Nike.DesignPattern.FacadePattern
{
    public class OrderFacade
    {
        private readonly QuanLySanPhamEntities _db;

        public OrderFacade(QuanLySanPhamEntities db)
        {
            _db = db;
        }

        public List<Order> GetOrders(string searchStr, string sort)
        {
            IQueryable<Order> query = _db.Orders.Include(o => o.KhachHang);

            // Xử lý tìm kiếm
            if (!string.IsNullOrEmpty(searchStr))
            {
                searchStr = searchStr.ToLower();
                query = query.Where(p => p.KhachHang.Sdt.ToString().ToLower().Contains(searchStr));
            }

            // Xử lý sort
            switch (sort)
            {
                case "Wait":
                    query = query.Where(s => s.Status == "Chưa giao hàng");
                    break;
                case "Deli":
                    query = query.Where(s => s.Status == "Đang giao hàng");
                    break;
                case "Done":
                    query = query.Where(s => s.Status == "Hoàn thành");
                    break;
                case "Cancel":
                    query = query.Where(s => s.Status == "Đã hủy");
                    break;
                default:
                    query = query.OrderBy(s => s.NgayDat);
                    break;
            }

            // Xử lý tự động cập nhật trạng thái đơn hàng
            var ordersToUpdate = query.Where(o => o.NgayGiao < DateTime.Now && o.Status == "Đang giao hàng").ToList();
            foreach (var order in ordersToUpdate)
            {
                order.Status = "Hoàn thành";
                order.Payment = true;
                _db.Entry(order).State = EntityState.Modified;
            }
            _db.SaveChanges();

            return query.ToList();
        }

        public Order GetOrderDetail(int id)
        {
            return _db.Orders.Find(id);
        }

        public Order GetOrderForEdit(int id)
        {
            return _db.Orders.Find(id);
        }

        public Order GetOrderForDelete(int id)
        {
            return _db.Orders.FirstOrDefault(r => r.ID == id);
        }

        public void ProcessOrder(int orderId)
        {
            var order = _db.Orders.Find(orderId);
            if (order == null)
                throw new ArgumentException("Đơn hàng không tồn tại");

            order.Status = "Đang giao hàng";
            order.NgayGiao = DateTime.Now.AddDays(3);

            foreach (var detail in order.Order_Detail)
            {
                var product = _db.Products.Find(detail.ID_Product);
                product.SoLuong -= 1;
                product.ProductSold += 1;
            }

            _db.SaveChanges();
        }

        public void CancelOrder(int orderId)
        {
            var order = _db.Orders.FirstOrDefault(r => r.ID == orderId);
            if (order == null)
                throw new ArgumentException("Đơn hàng không tồn tại");

            _db.Orders.Remove(order);
            _db.SaveChanges();
        }
    }
}