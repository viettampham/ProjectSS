using System;
using System.Collections.Generic;

namespace ProjectSS.Models
{
    public class OrderDetail
    {
        public Guid id { get; set; }
        public Product Product { get; set; }
        public int QuantityAvailable { get; set; }
        public int SizeOrder { get; set; }
        public string BrandOrder { get; set; }
        public string CategoryOrder { get; set; }
    }
}