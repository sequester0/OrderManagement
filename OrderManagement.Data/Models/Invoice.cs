namespace OrderManagement.Data.Models
{
    public partial class Invoice
    {
        public int OrderId { get; set; }
        public int? AddressId { get; set; }
    }
}
