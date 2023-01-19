using System;

namespace ProjectSS.Models.RequestModels
{
    public class CreateOrderRequest
    {
        public Guid UserID  { get; set; }
        public Guid ProductId  { get; set; }
        public int Quantity  { get; set; }
    }
}