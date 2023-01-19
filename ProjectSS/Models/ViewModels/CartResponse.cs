using System;
using System.Collections.Generic;

namespace ProjectSS.Models.ViewModels
{
    public class CartResponse
    {
        public Guid cartId { get; set; }
        public Guid UserID { get; set; }
        public List<ListOrderResponse> OrderDetails { get; set; }
        public int TotalCart { get; set; }
    }
}