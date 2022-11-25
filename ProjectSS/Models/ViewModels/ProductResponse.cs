using System;
using System.Collections.Generic;

namespace ProjectSS.Models.ViewModels
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
        public string image_url { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public int QuantityaVailable { get; set; }
        public string Brand { get; set; }
        public List<Category> Categorys  { get; set; }
    }

    
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
        public string image_url { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public int QuantityaVailable { get; set; }
        public string Brand { get; set; }
        public List<CategoryTempo> Categorys  { get; set; }
    }
    
    
    public class GetListProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
        public string image_url { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public int QuantityaVailable { get; set; }
        public string Brand { get; set; }
        public List<CategoryTempo> Categorys  { get; set; }
    }

    public class CategoryTempo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }    
    }
}