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
            var result = _orderBusinessEngine.GetOrderById(orderid);
            if (!result.Status)
            {
                return BadRequest(result);
            }          
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<OrderBusinessEngine> CreateOrderItem(OrderCreateDto orderCreateDto)
        {
            var result = _orderBusinessEngine.Add(orderCreateDto);
            if (!result.Status)
            {
                return BadRequest(result);
            }
            //_orderBusinessEngine.Add(orderCreateDto);
            return Ok(result);
        
        }

        [HttpDelete("{orderid}")]
        public ActionResult DeleteOrderItem(int orderid)
        {
            var result= _orderBusinessEngine.Remove(orderid);
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
