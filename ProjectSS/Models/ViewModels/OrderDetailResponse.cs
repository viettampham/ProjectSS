using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectSS.Models.ViewModels
{
    public class OrderDetailResponse
    {
        public Guid id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        
        
        /*public string SizeOrder { get; set; }
        public string BrandOrder { get; set; }
        public string CategoryOrder { get; set; }*/
    }
    
    public class EditOrderResponse
    {
        public Guid id { get; set; }
        public ProductOrder ProductOrder { get; set; }
        public int Quantity { get; set; }
        public int TotalMoney { get; set; }
    }
    
    public class ListOrderResponse
    {
        public Guid id { get; set; }
        public ProductOrder ProductOrder { get; set; }
        public int Quantity { get; set; }
        public int TotalMoney { get; set; }
    }
}