using System;
using System.Collections.Generic;

namespace ProjectSS.Models
{
    public class Category
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public List<Product> Products { get; set; }
    }
}