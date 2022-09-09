using OrderManagement.Data.Models;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OrderManagement.Common.DTO.Order
{
    [DataContract]
    public class OrderDto
    {
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int? UserId { get; set; }
        [DataMember]
        public int? ProductId { get; set; }
        [DataMember]
        public DateTime? OrderDate { get; set; }
        [DataMember]
        public string CurrentOrderStatus
        {
            get { return OrderStatus.ToString(); }
            set { CurrentOrderStatus = value; }
        }
        [DataMember]
        public string? BrandName { get; set; }
        [DataMember]
        public string? ProductName { get; set; }
        [DataMember]
        public string? OrderAddress { get; set; }
        [DataMember]
        public string? InvoiceAddress { get; set; }

        [JsonIgnore]
        public OrderStatus OrderStatus { get; set; }
    }
}
