using OrderManagement.Data.Models;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BusinessEngine
{
    public class OrderBusinessEngine : IOrderBusinessEngine
    {
        EGITIM_TESTContext _context;

        public OrderBusinessEngine(EGITIM_TESTContext context)
        {
            _context = context;
        }

        public List<Order> Get()
        {
            return _context.Orders.ToList();
        }

        public void Add(OrderCreateDto orderCreateDto)
        {
            var finalOrderItem = new Order()
            {
                CustomerId = orderCreateDto.CustomerId
            };
            _ = new Order()
            {
                ProductId = orderCreateDto.ProductId
            };
            _ = new Order()
            {
                OrderDate = orderCreateDto.OrderDate
            };
            _context.Orders.Add(finalOrderItem);
            _context.SaveChanges();
        }

        public Order GetOrderById(int orderid)
        {
            var order = _context.Orders.FirstOrDefault(b => b.OrderId == orderid);
            return order;
        }
    }
}
