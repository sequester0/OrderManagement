using System;
using System.Collections.Generic;

namespace OrderManagement.Data.Models
{
    public partial class Basket
    {
        public int BasketId { get; set; }
        public int? ProductId { get; set; }
    }
}
