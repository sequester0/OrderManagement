using OrderManagement.Common.Contracts;
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
    public class ProductBusinessEngine : IProductBusinessEngine
    {
        EGITIM_TESTContext _context;

        public ProductBusinessEngine(EGITIM_TESTContext context)
        {
            _context = context;
        }

        public List<Product> Get()
        {
            return _context.Products.ToList();
        }

        public Result<Product> Add(ProductCreateDto productCreateDto)
        {
            try
            {
                var finalProductItem = new Product()
                {
                    ProductName = productCreateDto.ProductName,
                    BrandId = productCreateDto.BrandId
                };

                _context.Products.Add(finalProductItem);
                _context.SaveChanges();
                return new Result<Product> { Data = finalProductItem, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Product> { Data = null , Message = ex.Message, Status = false };
            }
        }

        public Result<ProductDto> GetProductById(int productid)
        {
            try
            {
                var product = _context.Products.First(p => p.ProductId == productid);
                var productDto = new ProductDto()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    BrandId = (int)product.BrandId
                };
                return new Result<ProductDto> { Data = productDto, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<ProductDto> { Message = ex.Message, Status = false };
            }
        }

        public Result<Product> Remove(int productid)
        {
            try
            {
                var productItemToRemove = _context.Products.First(b => b.ProductId == productid);

                _context.Products.Remove(productItemToRemove);
                _context.SaveChanges();
                return new Result<Product> { Data = productItemToRemove, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Product> { Message = ex.Message, Status = false };
            }
        }
    }
}
