using Microsoft.EntityFrameworkCore;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Product;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using Serilog;

namespace OrderManagement.BusinessEngine
{
    public class ProductBusinessEngine : IProductBusinessEngine
    {
        EGITIM_TESTContext _context;

        public ProductBusinessEngine(EGITIM_TESTContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Product>>> Get()
        {
            Log.Information("Getting all products");
            var products = await _context.Products.ToListAsync();
            return new Result<List<Product>> { Data = products, Message = "Operation successful", Status = true };
        }

        public async Task<Result<Product>> Add(ProductCreateDto productCreateDto)
        {
            Log.Information("Adding product");

            var finalProductItem = new Product()
            {
                ProductName = productCreateDto.ProductName,
                BrandId = productCreateDto.BrandId
            };

            await _context.Products.AddAsync(finalProductItem);
            await _context.SaveChangesAsync();

            return new Result<Product> { Data = finalProductItem, Message = "Operation successful", Status = true };
        }

        public async Task<Result<ProductDto>> GetProductById(int productid)
        {
            Log.Information($"The product with id number {productid} is getting.");

            var product = await _context.Products.FirstAsync(p => p.ProductId == productid);
            
            if (product == null)
            {
                Log.Error($"The product with id number {productid} couldn't found while trying to get.");
                throw new KeyNotFoundException("Product not found. Try again!");
            }
            
            var productDto = new ProductDto()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                BrandId = product.BrandId
            };

            return new Result<ProductDto> { Data = productDto, Message = "Operation successful", Status = true};
        }

        public async Task<Result<Product>> Remove(int productid)
        {
            Log.Information($"The product with id number {productid} is being deleted.");

            var productItemToRemove = await _context.Products.FirstAsync(x => x.ProductId == productid);

            if (productItemToRemove == null)
            {
                Log.Error($"The product with id number {productid} couldn't found while trying to deleted.");
                throw new KeyNotFoundException("Product not found. Try again!");
            }
            
            _context.Products.Remove(productItemToRemove);
            await _context.SaveChangesAsync();

            return new Result<Product> { Data = productItemToRemove, Message = "Operation successful", Status = true };
        }
    }
}
