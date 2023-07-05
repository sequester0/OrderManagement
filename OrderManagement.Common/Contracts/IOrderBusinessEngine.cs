using Microsoft.AspNetCore.JsonPatch;
using OrderManagement.Common.DTO.Order;
using OrderManagement.Common.Helpers;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;

namespace OrderManagement.Common.Contracts
{
    public interface IOrderBusinessEngine
    {
        (Result<IEnumerable<dynamic>>, PaginationMetadata) GetOrders(int userid, int pageNumber, int pageSize);
        Task<Result<OrderDto>> GetOrderById(int orderid);
        Task<Result<List<Order>>> CreateOrder(int userid, OrderCreateDto orderCreateDto);
        Task<Result<Order>> UpdateOrderStatus(int orderid, JsonPatchDocument<OrderPartialUpdateDto> jsonPatchDocument);
    }
}
