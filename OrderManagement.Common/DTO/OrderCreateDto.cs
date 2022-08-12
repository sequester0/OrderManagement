using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Common.DTO
{
    public class OrderCreateDto
    {
        //public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
