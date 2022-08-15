using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Brand;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        IBrandBusinessEngine _brandBusinessEngine;

        public BrandController(IBrandBusinessEngine brandBusinessEngine)
        {
            _brandBusinessEngine = brandBusinessEngine;
        }

        [HttpGet]
        public ActionResult GetBrands()
        {
            var result = _brandBusinessEngine.GetBrands();
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{brandid}")]
        public ActionResult GetBrandById(int brandid)
        {
            var result = _brandBusinessEngine.GetBrandById(brandid);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateBrand(BrandCreateDto brandCreateDto)
        {
            var result = _brandBusinessEngine.Add(brandCreateDto);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{brandid}")]
        public ActionResult UpdateName(int brandid, BrandCreateDto brandCreateDto)
        {
            var result = _brandBusinessEngine.UpdateBrandName(brandid, brandCreateDto);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{brandid}")]
        public ActionResult Remove(int brandid)
        {
            var result = _brandBusinessEngine.Remove(brandid);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
