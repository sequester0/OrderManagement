using OrderManagement.Data.Models;
using OrderManagement.Common.DTO;

namespace OrderManagement.Common.Contracts
{
    public interface IOrderBusinessEngine
    {
        Order GetOrderById(int orderid);
        List<Order> Get();
        void Add(OrderCreateDto orderCreatDto);
    }
}
