using System;
using System.Collections.Generic;

namespace ProjectSS.Models.ViewModels
{
    public class CartResponse
    {
        public Guid cartId { get; set; }
        public List<ListOrderResponse> OrderDetails { get; set; }
    }
}