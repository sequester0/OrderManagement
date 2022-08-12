using OrderManagement.Data.Models;
using OrderManagement.Common.DTO;
using OrderManagement.Common.Result;

namespace OrderManagement.Common.Contracts
{
    public interface IOrderBusinessEngine
    {
        Result<OrderDto> GetOrderById(int orderid);
        List<Order> Get();
        Result<Order> Add(OrderCreateDto orderCreatDto);
        Result<Order> Remove(int productid);
    }
}
