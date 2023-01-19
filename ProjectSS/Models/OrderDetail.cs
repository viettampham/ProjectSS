using System;
using System.Collections.Generic;

namespace ProjectSS.Models
{
    public class OrderDetail
    {
        public Guid id { get; set; }
        public Guid UserID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int TotalMoney { get; set; }
        public List<Cart> Carts { get; set; }
    
    }
}