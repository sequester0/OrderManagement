using OrderManagement.Data.Models;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Common.Result;

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

        public Result<Order> Add(OrderCreateDto orderCreateDto)
        {
            try
            {
                var finalOrderItem = new Order()
                {                   
                    CustomerId = orderCreateDto.CustomerId,
                    ProductId = orderCreateDto.ProductId,
                    OrderDate = orderCreateDto.OrderDate
                };
                _context.Orders.Add(finalOrderItem);
                _context.SaveChanges();

                return new Result<Order> { Data = finalOrderItem, Message = "Open succesfully", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Order> { Data = null, Message = ex.Message, Status = false };
            }

        }

        public Result<OrderDto> GetOrderById(int orderid)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(b => b.OrderId == orderid);
                var orderDto = new OrderDto()
                {
                    OrderId = order.OrderId,
                };

                return new Result<OrderDto> { Data = orderDto, Message = "Open succesfully", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<OrderDto> { Message = ex.Message, Status = false };

            }
        }

        public Result<Order> Remove(int orderid)
        {
            try
            {
                var orderItemToRemove = _context.Orders.First(b => b.OrderId == orderid);

                _context.Orders.Remove(orderItemToRemove);
                _context.SaveChanges();

                return new Result<Order> { Data = orderItemToRemove, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Order> { Message = ex.Message, Status = false };
            }
        }
    }
}
