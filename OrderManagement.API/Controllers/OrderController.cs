using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.BusinessEngine;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO;
using OrderManagement.Data.Models;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderBusinessEngine _orderBusinessEngine;

        public OrderController(IOrderBusinessEngine orderBusinessEngine)
        {
            _orderBusinessEngine = orderBusinessEngine;
        }

        [HttpGet]
        public ActionResult GetOrders()
        {
            var orderList = _orderBusinessEngine.Get();
            return Ok(orderList);
        }

        [HttpGet("{orderid}")]
        public ActionResult GetOrder(int orderid)
        {
            var order = _orderBusinessEngine.GetOrderById(orderid);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public ActionResult<OrderBusinessEngine> CreateBasketItem(OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto == null)
            {
                return NotFound();
            }

            _orderBusinessEngine.Add(orderCreateDto);
            return Ok(orderCreateDto);
        
        }


    }
}
