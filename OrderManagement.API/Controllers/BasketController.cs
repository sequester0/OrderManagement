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

        [HttpGet("{basketid}")]
        public ActionResult GetBasket(int basketid)
        {
            var basket = _basketBusinessEngine.GetBasketById(basketid);
            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpPost]
        public ActionResult<BasketCreateDto> CreateBasketItem(BasketCreateDto basketCreateDto)
        {
            if (basketCreateDto == null)
            {
                return NotFound();
            }

            //var maxBasketId = _basketBusinessEngine.Get().Max(c => c.BasketId);

            _basketBusinessEngine.Add(basketCreateDto);
            return Ok(basketCreateDto);
            //return CreatedAtRoute(nameof(CreateBasketItem)w, new {basketCreateDto = finalBasketItem} ,finalBasketItem);
        }
    }
}
