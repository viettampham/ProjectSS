using System;
using System.Collections.Generic;

namespace ProjectSS.Models.RequestModels
{
    public class CreateCartRequest
    {
        public Guid UserID { get; set; }
        public List<Guid> OrderDetailsId { get; set; }
    }
}