using System;
using System.Collections.Generic;

namespace ProjectSS.Models.ViewModels
{
    public class BillResponse
    {
        public Guid id { get; set; }
        /*public CartResponse Cart { get; set; }*/
        public List<ListOrderResponse> ListOrderResponses { get; set; }
        public ApplicationUser User { get; set; }
        public string NameCustomerOrder { get; set; }
        public string PhoneNumberCustomer { get; set; }
        public string Address { get; set; }
        public string status { get; set; }
    }
}