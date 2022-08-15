using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Brand;
using OrderManagement.Common.DTO.Product;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BusinessEngine
{
    public class BrandBusinessEngine : IBrandBusinessEngine
    {
        EGITIM_TESTContext _context;
        public BrandBusinessEngine(EGITIM_TESTContext context)
        {
            _context = context;
        }

        public Result<IEnumerable<BrandDto>> GetBrands()
        {
            try
            {
                var query = (from brandq in _context.Brands
                                 join products in _context.Products on brandq.BrandId equals products.BrandId into pr
                                 from x in pr.DefaultIfEmpty()
                                 select new
                                 {
                                     BrandId = brandq.BrandId,
                                     BrandName = brandq.BrandName,
                                     Product = (x == null ? null : new ProductGetDto { ProductId = x.ProductId, ProductName = x.ProductName })
                                 });

                var brands = query.GroupBy(x => new { x.BrandId, x.BrandName }).Select(x => new BrandDto
                {
                    BrandId = x.Key.BrandId,
                    BrandName = x.Key.BrandName,
                    Products = x.Select(p => p.Product == null ? null : new ProductGetDto
                    {
                        ProductId = p.Product.ProductId,
                        ProductName = p.Product.ProductName,
                    }).ToList()
                }).ToList();

                return new Result<IEnumerable<BrandDto>> { Data = brands, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<BrandDto>> { Message = ex.Message, Status = false};
            }
        }

        public Result<IEnumerable<BrandDto>> GetBrandById(int brandid)
        {
            try
            {
                var brand = (from products in _context.Products
                             join brands in _context.Brands on products.BrandId equals brands.BrandId
                             where brands.BrandId == brandid
                             select new { 
                                 BrandId = brands.BrandId,
                                 BrandName = brands.BrandName,
                                 Product = new ProductGetDto { ProductId = products.ProductId, ProductName = products.ProductName },
                             }).ToList();

                var group = brand.GroupBy(x => new { x.BrandId, x.BrandName }).Select(x => new BrandDto 
                { 
                    BrandId = x.Key.BrandId, 
                    BrandName = x.Key.BrandName, 
                    Products = x.Select(p => new ProductGetDto
                    { 
                        ProductId = p.Product.ProductId,
                        ProductName = p.Product.ProductName
                    }).ToList()
                });

                return new Result<IEnumerable<BrandDto>> { Data = group, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<BrandDto>> { Message = ex.Message, Status = false };
            }
        }

        public Result<Brand> Add(BrandCreateDto brandCreateDto)
        {
            try
            {
                var newBrand = new Brand
                {
                    BrandName = brandCreateDto.BrandName,
                };
                _context.Brands.Add(newBrand);
                _context.SaveChanges();
                return new Result<Brand> { Data = newBrand, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Brand> { Message = ex.Message, Status = false };
            }
        }

        public Result<dynamic> UpdateBrandName(int brandid, BrandCreateDto brandCreateDto)
        {
            try
            {
                var brand = _context.Brands.First(x => x.BrandId == brandid);
                brand.BrandName = brandCreateDto.BrandName;
                _context.SaveChanges();
                return new Result<dynamic> { Data = brand, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<dynamic> { Message = ex.Message, Status = false };
            }
        }

        public Result<Brand> Remove(int brandid)
        {
            try
            {
                var brand = _context.Brands.First(x => x.BrandId == brandid);
                var products = _context.Products.Where(x => x.BrandId == brandid).ToList();
                if (products != null)
                {
                    _context.Products.RemoveRange(products);
                }
                _context.Brands.Remove(brand);
                _context.SaveChanges();
                return new Result<Brand> { Data = brand, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Brand> { Message = ex.Message, Status = false };
            }
        }
    }
}
