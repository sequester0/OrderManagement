using Microsoft.AspNetCore.Mvc;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Brand;
using OrderManagement.Common.Helpers;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        IBrandBusinessEngine _brandBusinessEngine;

        public BrandController(IBrandBusinessEngine brandBusinessEngine)
        {
            _brandBusinessEngine = brandBusinessEngine;
        }

        [HttpGet]
        public async Task<ActionResult> GetBrands()
        {
            var result = await _brandBusinessEngine.GetBrands();
            return Ok(result);
        }

        [HttpGet("{brandid}")]
        public async Task<ActionResult> GetBrandById(int brandid)
        {
            var result = await _brandBusinessEngine.GetBrandById(brandid);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBrand(BrandCreateDto brandCreateDto)
        {
            var result = await _brandBusinessEngine.Add(brandCreateDto);
            return Ok(result);
        }

        [HttpPut("{brandid}")]
        public async Task<ActionResult> UpdateName(int brandid, BrandCreateDto brandCreateDto)
        {
            var result = await _brandBusinessEngine.UpdateBrandName(brandid, brandCreateDto);
            return Ok(result);
        }

        [HttpDelete("{brandid}")]
        public async Task<ActionResult> Remove(int brandid)
        {
            var result = await _brandBusinessEngine.Remove(brandid);
            return Ok(result);
        }

    }
}
