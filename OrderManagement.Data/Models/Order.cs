namespace OrderManagement.Data.Models
{
    public enum OrderStatus
    {
        OrderReceived,
        OrderInProgress,
        OrderSent,
        OrderCancelled
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? OrderAddress { get; set; }
        public string? InvoiceAddress { get; set; }
    }

}
