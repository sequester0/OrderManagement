using Microsoft.EntityFrameworkCore;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Brand;
using OrderManagement.Common.DTO.Product;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using Serilog;

namespace OrderManagement.BusinessEngine
{
    public class BrandBusinessEngine : IBrandBusinessEngine
    {
        EGITIM_TESTContext _context;
        public BrandBusinessEngine(EGITIM_TESTContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<BrandDto>>> GetBrands()
        {
            Log.Information("Getting all brands.");

            var query = await (from brandq in _context.Brands
                         join products in _context.Products on brandq.BrandId equals products.BrandId into pr
                         from x in pr.DefaultIfEmpty()
                         select new
                         {
                             BrandId = brandq.BrandId,
                             BrandName = brandq.BrandName,
                             Products = x
                         }).ToListAsync();

            var brands = query.GroupBy(x => new { x.BrandId, x.BrandName });

            var brandsList = GetBrandList(brands);

            return new Result<IEnumerable<BrandDto>> { Data = brandsList, Message = "Operation successful", Status = true };
        }

        public async Task<Result<IEnumerable<BrandDto>>> GetBrandById(int brandid)
        {
            Log.Information($"The brand with id number {brandid} is getting.");

            var brand = await (from brands in _context.Brands
                         join products in _context.Products on brands.BrandId equals products.BrandId into pr
                         from x in pr.DefaultIfEmpty()
                         where brands.BrandId == brandid
                         select new
                         {
                             BrandId = brands.BrandId,
                             BrandName = brands.BrandName,
                             Products = x,
                         }).ToListAsync();

            var group = brand.GroupBy(x => new { x.BrandId, x.BrandName });

            var brandList = GetBrandList(group);

            if (brandList.Count <= 0)
                throw new KeyNotFoundException($"There are no brand with id {brandid}");

            return new Result<IEnumerable<BrandDto>> { Data = brandList, Message = "Operation successful", Status = true };
        }

        public async Task<Result<Brand>> Add(BrandCreateDto brandCreateDto)
        {
            var newBrand = new Brand
            {
                BrandName = brandCreateDto.BrandName,
            };

            Log.Information($"Adding brand with name {newBrand.BrandName}");

            await _context.Brands.AddAsync(newBrand);
            await _context.SaveChangesAsync();
            return new Result<Brand> { Data = newBrand, Message = "Operation successful", Status = true };
        }

        public async Task<Result<Brand>> UpdateBrandName(int brandid, BrandCreateDto brandCreateDto)
        {
            Log.Information($"Updating brand name with id number {brandid} to {brandCreateDto.BrandName}");

            var brand = await _context.Brands.FirstAsync(x => x.BrandId == brandid);

            brand.BrandName = brandCreateDto.BrandName;
            await _context.SaveChangesAsync();

            return new Result<Brand> { Data = brand, Message = "Operation successful", Status = true };
        }

        public async Task<Result<Brand>> Remove(int brandid)
        {
            Log.Information($"The brand with id number {brandid} and brand products is deleting.");

            var brand = await _context.Brands.FirstAsync(x => x.BrandId == brandid);
            var products = await _context.Products.Where(x => x.BrandId == brandid).ToListAsync();

            if (products != null)
            {
                _context.Products.RemoveRange(products);
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return new Result<Brand> { Data = brand, Message = "Operation successful", Status = true };
        }

        private List<BrandDto> GetBrandList(IEnumerable<IGrouping<dynamic, dynamic>> values)
        {
            List<BrandDto> brandsList = new List<BrandDto>();
            foreach (var x in values)
            {
                var brand = new BrandDto
                {
                    BrandId = x.Key.BrandId,
                    BrandName = x.Key.BrandName,
                    Products = new List<ProductGetDto>()
                };

                foreach (var item in x)
                {
                    if (item.Products != null)
                    {
                        ProductGetDto product = new ProductGetDto
                        {
                            ProductId = item.Products.ProductId,
                            ProductName = item.Products.ProductName
                        };

                        brand.Products.Add(product);
                    }
                }

                brandsList.Add(brand);
            }
            return brandsList;
        }

    }
}
