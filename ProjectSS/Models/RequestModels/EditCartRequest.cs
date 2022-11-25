using System;
using System.Collections.Generic;

namespace ProjectSS.Models.RequestModels
{
    public class EditCartRequest
    {
        public Guid cartId { get; set; }
        public List<Guid> OrderDetailsId { get; set; }
    }
}