using OrderManagement.Common.DTO.Product;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Common.Contracts
{
    public interface IProductBusinessEngine
    {
        List<Product> Get();
        Result<ProductDto> GetProductById(int productid);
        Result<Product> Add(ProductCreateDto productCreateDto);
        Result<Product> Remove(int productid);
    }
}
