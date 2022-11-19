using System;
using System.Collections.Generic;

namespace ProjectSS.Models
{
    public class Cart
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}