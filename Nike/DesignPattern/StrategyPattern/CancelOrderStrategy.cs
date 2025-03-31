using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nike.Models;

namespace Nike.DesignPattern.StrategyPattern
{
    public class CancelOrderStrategy : IOrderStatusStrategy
    {
        public void ProcessOrder(Order order)
        {
            order.Status = "Đã hủy";
        }
    }
}