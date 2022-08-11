using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Common.Result
{
    public class Result
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
    }

    public class Result<T>
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public T? Data { get; set; }
    }
}
