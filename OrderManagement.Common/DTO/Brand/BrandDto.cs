using OrderManagement.Common.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Common.DTO.Brand
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public ICollection<ProductGetDto>? Products { get; set; }
    }
}
