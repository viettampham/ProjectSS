using System;
using System.Collections.Generic;

namespace ProjectSS.Models.RequestModels
{
    public class EditCategoryRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Guid> ProductID { get; set; }
    }
}