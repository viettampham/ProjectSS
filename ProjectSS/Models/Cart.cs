using System;
using System.Collections.Generic;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Models
{
    public class Cart
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public int TotalCart { get; set; }

        public Bill Bill { get; set; }
    }
}