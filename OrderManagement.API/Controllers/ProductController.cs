using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Product;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductBusinessEngine _productBusinessEngine;

        public ProductController(IProductBusinessEngine productBusinessEngine)
        {
            _productBusinessEngine = productBusinessEngine;
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            var productList = _productBusinessEngine.Get();
            return Ok(productList);
        }

        [HttpGet("{productid}")]
        public ActionResult GetProduct(int productid)
        {
            var result = _productBusinessEngine.GetProductById(productid);
            if (!result.Status)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductCreateDto productCreateDto)
        {
            var result = _productBusinessEngine.Add(productCreateDto);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{productid}")]
        public ActionResult DeleteProductItem(int productid)
        {
            var result = _productBusinessEngine.Remove(productid);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
