using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.BusinessEngine;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Basket;
using OrderManagement.Data.Models;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        IBasketBusinessEngine _basketBusinessEngine;

        public BasketController(IBasketBusinessEngine basketBusinessEngine)
        {
            _basketBusinessEngine = basketBusinessEngine;
        }

        [HttpGet]
        public ActionResult GetBaskets()
        {
            var basketList = _basketBusinessEngine.Get();
            return Ok(basketList);
        }

        [HttpGet("{basketid}", Name = "GetBasket")]
        public ActionResult GetBasket(int basketid)
        {
            var basket = _basketBusinessEngine.GetBasketById(basketid);
            if (!basket.Status)
            {
                return BadRequest(basket);
            }

            return Ok(basket);
        }

        //[HttpPatch("{userid}")]
        //public ActionResult UpdateBasket(int userid, BasketUpdateDto basketUpdateDto)
        //{
        //    var result = _basketBusinessEngine.Update(userid, basketUpdateDto);
        //    if (!result.Status)
        //    {
        //        return BadRequest(result);
        //    }

        //    return Ok(result);
        //}

        [HttpPost]
        public ActionResult<BasketCreateDto> CreateBasketItem(BasketCreateDto basketCreateDto)
        {
            var result = _basketBusinessEngine.Add(basketCreateDto);
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{productid}")]
        public ActionResult DeleteBasketItem(int productid)
        {
            var result = _basketBusinessEngine.Remove(productid);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
