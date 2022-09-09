using OrderManagement.Common.DTO.Product;

namespace OrderManagement.Common.DTO.Brand
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public List<ProductGetDto> Products { get; set; }
    }
}
