﻿namespace OrderManagement.Data.Models
{
    public partial class Basket
    {
        public int BasketId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
    }
}
