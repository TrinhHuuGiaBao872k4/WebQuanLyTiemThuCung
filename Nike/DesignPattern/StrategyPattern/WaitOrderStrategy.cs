using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nike.Models;

namespace Nike.DesignPattern.StrategyPattern
{
    public class WaitOrderStrategy : IOrderStatusStrategy
    {
        public void ProcessOrder(Order order)
        {
            order.Status = "Chưa giao hàng";
        }
    }
}