using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Order;
using OrderManagement.Common.Helpers;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using Serilog;

namespace OrderManagement.BusinessEngine
{
    public class OrderBusinessEngine : IOrderBusinessEngine
    {
        EGITIM_TESTContext _context;
        IMapper _mapper;

        public OrderBusinessEngine(EGITIM_TESTContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public (Result<IEnumerable<dynamic>>, PaginationMetadata) GetOrders(int userid, int pageNumber, int pageSize)
        {
            Log.Information($"Getting user orders with user id number {userid}, pageSize: {pageSize}, pageNumber: {pageNumber}");

            var query = (from ordersq in _context.Orders
                         join products in _context.Products on ordersq.ProductId equals products.ProductId
                         where ordersq.UserId == userid
                         select new
                         {
                             OrderId = ordersq.OrderId,
                             UserId = ordersq.UserId,
                             ProductId = ordersq.ProductId,
                             OrderDate = ordersq.OrderDate,
                             OrderStatus = ordersq.OrderStatus,
                             ProductName = products.ProductName,
                             BrandId = products.BrandId,
                             OrderAddress = ordersq.OrderAddress,
                             InvoiceAddress = ordersq.InvoiceAddress
                         });

            var orders = (from que in query
                          join brand in _context.Brands on que.BrandId equals brand.BrandId
                          select new
                          {
                              OrderId = que.OrderId,
                              UserId = que.UserId,
                              OrderDate = que.OrderDate,
                              OrderStatus = que.OrderStatus,
                              ProductName = que.ProductName,
                              ProductId = que.ProductId,
                              BrandName = brand.BrandName,
                              OrderAddress = que.OrderAddress,
                              InvoiceAddress = que.InvoiceAddress
                          }).OrderByDescending(x => x.OrderDate)
                            .AsEnumerable()
                            .Select(x => new OrderDto
                            {
                                OrderId = x.OrderId,
                                UserId = x.UserId,
                                ProductId = x.ProductId,
                                OrderDate = x.OrderDate,
                                BrandName = x.BrandName,
                                ProductName = x.ProductName,
                                OrderStatus = x.OrderStatus,
                                OrderAddress = x.OrderAddress,
                                InvoiceAddress = x.InvoiceAddress

                            })
                            .GroupBy(x => new { x.UserId, x.OrderDate });


            var totalItemCount = orders.Count();
            var pagionationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);
            var orderToReturn = orders.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            return (new Result<IEnumerable<dynamic>> { Data = orderToReturn, Message = "Operation successful", Status = true }, pagionationMetadata);
        }

        public async Task<Result<OrderDto>> GetOrderById(int orderid)
        {
            Log.Information($"Getting order with order id number {orderid}");

            var query = (from ordersq in _context.Orders
                         join products in _context.Products on ordersq.ProductId equals products.ProductId
                         where ordersq.OrderId == orderid
                         select new
                         {
                             OrderId = ordersq.OrderId,
                             UserId = ordersq.UserId,
                             ProductId = ordersq.ProductId,
                             OrderDate = ordersq.OrderDate,
                             OrderStatus = ordersq.OrderStatus,
                             ProductName = products.ProductName,
                             BrandId = products.BrandId,
                             OrderAddress = ordersq.OrderAddress
                         });

            var orderQuery = await (from que in query
                          join brand in _context.Brands on que.BrandId equals brand.BrandId
                          select new
                          {
                              OrderId = que.OrderId,
                              UserId = que.UserId,
                              OrderDate = que.OrderDate,
                              OrderStatus = que.OrderStatus,
                              ProductName = que.ProductName,
                              ProductId = que.ProductId,
                              BrandName = brand.BrandName,
                              OrderAddress = que.OrderAddress
                          }).FirstAsync();

            var order = new OrderDto()
            {
                OrderId = orderQuery.OrderId,
                UserId = orderQuery.UserId,
                ProductId = orderQuery.ProductId,
                OrderDate = orderQuery.OrderDate,
                BrandName = orderQuery.BrandName,
                ProductName = orderQuery.ProductName,
                OrderStatus = orderQuery.OrderStatus,
                OrderAddress = orderQuery.OrderAddress
            };

            return new Result<OrderDto> { Data = order, Message = "Operation successful", Status = true };
        }

        public async Task<Result<List<Order>>> CreateOrder(int userid, OrderCreateDto orderCreateDto)
        {
            Log.Information($"Creating order with user id number {userid}");

            var userBasket = await _context.Baskets.Where(x => x.UserId == userid).ToListAsync();

            List<Order> orderList = new List<Order>();
            foreach (var item in userBasket)
            {
                var order = new Order()
                {
                    UserId = userid,
                    ProductId = item.ProductId,
                    OrderDate = DateTime.Now,
                    OrderStatus = 0,
                    OrderAddress = orderCreateDto.OrderAddress,
                    InvoiceAddress = orderCreateDto.InvoiceAddress
                };

                orderList.Add(order);
            }

            await _context.Orders.AddRangeAsync(orderList);

            // Delete from Basket
            //_context.Baskets.RemoveRange(userBasket);
            
            await _context.SaveChangesAsync();
            
            return new Result<List<Order>> { Data = orderList, Message = "Operation successful", Status = true };
        }

        public async Task<Result<Order>> UpdateOrderStatus(int orderid, JsonPatchDocument<OrderPartialUpdateDto> jsonPatchDocument)
        {
            Log.Information($"Updating order status with order id number {orderid}");

            var order = await _context.Orders.FirstAsync(x => x.OrderId == orderid);
            var orderToPatch = _mapper.Map<OrderPartialUpdateDto>(order);
            jsonPatchDocument.ApplyTo(orderToPatch);
            _mapper.Map(orderToPatch, order);
            await _context.SaveChangesAsync();
            return new Result<Order> { Data = order, Message = "Operation successful", Status = true };
        }

    }
}
