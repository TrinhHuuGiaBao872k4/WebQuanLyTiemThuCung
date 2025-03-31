using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nike.Models;

namespace Nike.DesignPattern.StrategyPattern
{
    public interface IOrderStatusStrategy
    {
        void ProcessOrder(Order order);
    }
}