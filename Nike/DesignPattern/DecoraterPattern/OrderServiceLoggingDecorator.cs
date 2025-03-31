using Nike.Areas.Admin.Controllers;
using Nike.Models;
using System;
using System.Collections.Generic;

namespace  Nike.DesignPattern.DecoraterPattern
{
    public class OrderServiceLoggingDecorator : IOrderService
    {
        private readonly IOrderService _decorated;
        private readonly ILogger _logger;

        public OrderServiceLoggingDecorator(IOrderService decorated, ILogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public List<Order> GetOrders()
        {
            _logger.Log("Fetching orders...");
            try
            {
                var result = _decorated.GetOrders();
                _logger.Log($"Successfully retrieved {result.Count} orders");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log($"Error fetching orders: {ex.Message}");
                throw;
            }
        }
    }
}