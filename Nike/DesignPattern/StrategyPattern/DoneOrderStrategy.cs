﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nike.Models;

namespace Nike.DesignPattern.StrategyPattern
{
    public class DoneOrderStrategy : IOrderStatusStrategy
    {
        public void ProcessOrder(Order order)
        {
            order.Status = "Hoàn thành";
        }
    }
}