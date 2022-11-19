﻿using System;
using System.Collections.Generic;

namespace ProjectSS.Models
{
    public class Product
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string image_url { get; set; }
        public int quantityAvailable { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string Brand { get; set; }
        public List<Category> Categories { get; set; }
    }
}