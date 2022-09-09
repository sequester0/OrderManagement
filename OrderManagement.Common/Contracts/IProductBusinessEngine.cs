using OrderManagement.Common.DTO.Product;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;

namespace OrderManagement.Common.Contracts
{
    public interface IProductBusinessEngine
    {
        Task<Result<List<Product>>> Get();
        Task<Result<ProductDto>> GetProductById(int productid);
        Task<Result<Product>> Add(ProductCreateDto productCreateDto);
        Task<Result<Product>> Remove(int productid);
    }
}
