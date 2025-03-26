using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nike.Models;

namespace Nike.DesignPatterns.Proxy
{
    public class OrderServiceProxy : IOrderService
    {
        private OrderService _orderService;
        private QuanLySanPhamEntities _db;
        private KhachHang _currentUser;

        public OrderServiceProxy(QuanLySanPhamEntities db, KhachHang currentUser)
        {
            _db = db;
            _currentUser = currentUser;
            _orderService = new OrderService(db);
        }

        public void CancelOrder(int orderId)
        {
            var order = _db.Orders.Find(orderId);

            // Kiểm tra xem người dùng hiện tại có quyền hủy đơn hàng này không
            if (order != null && order.KhachHangID == _currentUser.idUser)
            {
                _orderService.CancelOrder(orderId);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không có quyền hủy đơn hàng này.");
            }
        }

        public Order GetOrderDetails(int orderId)
        {
            var order = _db.Orders.Find(orderId);

            // Kiểm tra xem người dùng hiện tại có quyền xem đơn hàng này không
            if (order != null && order.KhachHangID == _currentUser.idUser)
            {
                return _orderService.GetOrderDetails(orderId);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không có quyền xem đơn hàng này.");
            }
        }
    }
}