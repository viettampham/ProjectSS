using System;
using System.Collections.Generic;

namespace ProjectSS.Models
{
    public class Bill
    {
        public Guid id { get; set; }
        public Cart Cart { get; set; }
        public ApplicationUser User { get; set; }
        public string NameCustomerOrder { get; set; }
        public string PhoneNumberCustomer { get; set; }
        public string Address { get; set; }
        public string status { get; set; }
    }
}