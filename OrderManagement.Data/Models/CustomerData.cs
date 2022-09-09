namespace OrderManagement.Data.Models
{
    public partial class CustomerData
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerTc { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public string? CustomerPassword { get; set; }
        public DateTime? CustomerCreatedAt { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
