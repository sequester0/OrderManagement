﻿using OrderManagement.Common.DTO.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Common.DTO.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        //public ICollection<BrandDto> Brands { get; set; }
    }
}
