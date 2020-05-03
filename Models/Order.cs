using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int PrdId { get; set; }
        public int UserId { get; set; }
        public long SelectedQuantity { get; set; }
        public int OrderStatus { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Modified { get; set; }
    }
}
