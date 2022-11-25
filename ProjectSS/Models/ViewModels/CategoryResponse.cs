using System;
using System.Collections.Generic;

namespace ProjectSS.Models.ViewModels
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }    
        public List<Product> Products { get; set; }    
    }
}