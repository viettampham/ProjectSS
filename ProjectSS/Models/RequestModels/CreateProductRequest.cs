﻿using System;
using System.Collections.Generic;

namespace ProjectSS.Models.RequestModels
{
    public class CreateProductRequest
    {
        public string title { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public int quantityAvailable { get; set; }
        public int price { get; set; }
        public string size { get; set; }
        public string Brand { get; set; }
        public List<Guid> CategorieID { get; set; }
    }
}