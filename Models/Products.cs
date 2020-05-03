using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Models
{
    public partial class Products
    {
        public int PrdId { get; set; }
        public string ProductName { get; set; }
        public decimal? Productprice { get; set; }
        public decimal? ProductWeight { get; set; }
        public decimal? ProductHeight { get; set; }
        public byte[] Image { get; set; }
        public string FilePath { get; set; }
        public byte[] BarCode { get; set; }
        public long? AvailbleQty { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
