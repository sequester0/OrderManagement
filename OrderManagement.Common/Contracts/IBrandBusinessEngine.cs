using OrderManagement.Common.DTO.Brand;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;

namespace OrderManagement.Common.Contracts
{
    public interface IBrandBusinessEngine
    {
        Task<Result<IEnumerable<BrandDto>>> GetBrands();
        Task<Result<IEnumerable<BrandDto>>> GetBrandById(int brandid);
        Task<Result<Brand>> Add(BrandCreateDto brandCreateDto);
        Task<Result<Brand>> Remove(int brandid);
        Task<Result<Brand>> UpdateBrandName(int brandid, BrandCreateDto brandCreateDto);
    }
}
