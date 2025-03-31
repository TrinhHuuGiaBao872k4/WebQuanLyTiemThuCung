using Nike.Models;
using System.Collections.Generic;

namespace  Nike.DesignPattern.DecoraterPattern
{
    public interface IOrderService
    {
        List<Order> GetOrders();
    }
}