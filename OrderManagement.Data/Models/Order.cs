using System;
using System.Collections.Generic;

namespace OrderManagement.Data.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
