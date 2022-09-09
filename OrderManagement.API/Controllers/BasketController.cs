using Microsoft.AspNetCore.Mvc;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Basket;
using OrderManagement.Common.Helpers;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        IBasketBusinessEngine _basketBusinessEngine;

        public BasketController(IBasketBusinessEngine basketBusinessEngine)
        {
            _basketBusinessEngine = basketBusinessEngine;
        }

        [HttpGet]
        public async Task<ActionResult> GetBaskets()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value);

            var basketList = await _basketBusinessEngine.Get(userId);
            return Ok(basketList);
        }

        [HttpGet("{basketid}", Name = "GetBasket")]
        public async Task<ActionResult> GetBasket(int basketid)
        {
            var basket = await _basketBusinessEngine.GetBasketById(basketid);
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
        public async Task<ActionResult<BasketCreateDto>> CreateBasketItem(BasketCreateDto basketCreateDto)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value);

            var result = await _basketBusinessEngine.Add(basketCreateDto, userid);
            return Ok(result);
        }

        [HttpDelete("{productid}")]
        public async Task<ActionResult> DeleteBasketItem(int productid)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value);

            var result = await _basketBusinessEngine.Remove(productid, userid);
            return Ok(result);
        }
    }
}
