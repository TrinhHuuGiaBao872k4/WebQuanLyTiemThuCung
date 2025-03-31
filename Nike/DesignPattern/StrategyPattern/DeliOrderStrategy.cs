using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nike.Models;

namespace Nike.DesignPattern.StrategyPattern
{
    public class DeliOrderStrategy : IOrderStatusStrategy
    {
        public void ProcessOrder(Order order)
        {
            order.Status = "Đang giao hàng";
        }
    }
}