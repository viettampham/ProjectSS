using System;

namespace ProjectSS.Models.RequestModels
{
    public class CreateOrderRequest
    {
        public Guid ProductId  { get; set; }
        public int Quantity  { get; set; }
        /*public string SizeOrder  { get; set; }*/
        /*public string BrandOrder { get; set; }*/
        /*public string CategoryOrder { get; set; }*/
    }
}