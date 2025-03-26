using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nike.Models;

namespace Nike.DesignPatterns.Proxy
{
    public interface IOrderService
    {
        void CancelOrder(int orderId);
        Order GetOrderDetails(int orderId);
    }
}