using OrderManagement.Common.DTO.Brand;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Common.Contracts
{
    public interface IBrandBusinessEngine
    {
        Result<IEnumerable<BrandDto>> GetBrands();
        Result<IEnumerable<BrandDto>> GetBrandById(int brandid);
        Result<Brand> Add(BrandCreateDto brandCreateDto);
        Result<Brand> Remove(int brandid);
        Result<dynamic> UpdateBrandName(int brandid, BrandCreateDto brandCreateDto);
    }
}
