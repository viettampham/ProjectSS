using System;

namespace ProjectSS.Models.RequestModels
{
    public class EditOrderRequest
    {
        public Guid id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}