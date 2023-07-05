using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Order;
using System.Text.Json;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderBusinessEngine _orderBusinessEngine;
        const int maxPageSize = 20;

        public OrderController(IOrderBusinessEngine orderBusinessEngine)
        {
            _orderBusinessEngine = orderBusinessEngine;
        }

        [HttpGet]
        public ActionResult GetOrders(int pageNumber = 1, int pageSize = 10)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value);
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }
            var (orders, paginationMetadata) = _orderBusinessEngine.GetOrders(userId, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(orders);
        }

        [HttpGet("{orderid}")]
        public async Task<ActionResult> GetOrderByOrderId(int orderid)
        {
            var order = await _orderBusinessEngine.GetOrderById(orderid);
            return Ok(order);
        }

        [HttpPost("createorder")]
        public async Task<ActionResult> Create(OrderCreateDto orderCreateDto)
        {
            var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "id")?.Value);
            var userOrder = await _orderBusinessEngine.CreateOrder(userId, orderCreateDto);
            return Ok(userOrder);
        }

        [HttpPatch("{orderid}")]
        public async Task<ActionResult> UpdateOrderStatus(int orderid, JsonPatchDocument<OrderPartialUpdateDto> patchDocument)
        {
            var update = await _orderBusinessEngine.UpdateOrderStatus(orderid, patchDocument);
            return Ok(update);
        }
    }
}
