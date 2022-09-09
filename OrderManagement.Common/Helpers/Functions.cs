using OrderManagement.Common.DTO.Brand;
using OrderManagement.Common.DTO.Product;

namespace OrderManagement.Common.Helpers
{
    public class Functions
    {
        public static List<BrandDto> GetBrandList(IEnumerable<IGrouping<dynamic, dynamic>> values)
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
