using System;
using System.Collections.Generic;

namespace ProjectSS.Models.RequestModels
{
    public class CreateCategoryRequest
    {
        public string Tittle { get; set; }
        public List<Guid> ProductsID { get; set; }
    }
}