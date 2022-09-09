using Microsoft.AspNetCore.Mvc;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Product;
using OrderManagement.Common.Helpers;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        IProductBusinessEngine _productBusinessEngine;

        public ProductController(IProductBusinessEngine productBusinessEngine)
        {
            _productBusinessEngine = productBusinessEngine;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var productList = await _productBusinessEngine.Get();
            return Ok(productList);
        }

        [HttpGet("{productid}")]
        public async Task<ActionResult> GetProduct(int productid)
        {
            var result = await _productBusinessEngine.GetProductById(productid);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCreateDto productCreateDto)
        {
            var result = await _productBusinessEngine.Add(productCreateDto);

            return Ok(result);
        }

        [HttpDelete("{productid}")]
        public async Task<ActionResult> DeleteProductItem(int productid)
        {
            var result = await _productBusinessEngine.Remove(productid);

            return Ok(result);
        }
    }
}
