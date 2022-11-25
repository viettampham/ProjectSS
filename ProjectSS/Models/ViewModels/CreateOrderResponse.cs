using System;

namespace ProjectSS.Models.ViewModels
{
    public class CreateOrderResponse
    {
        public Guid id { get; set; }
        public ProductOrder Product { get; set; }
        public int Quantity { get; set; }
        public int TotalMoneyOrder { get; set; }
        /*public string SizeOrder { get; set; }
        public string BrandOrder { get; set; }
        public string CategoryOrder { get; set; }*/
    }
    
    public class ProductOrder
    {
        public string title { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public int price { get; set; }
        public string size { get; set; }
        public string Brand { get; set; }
    }
}