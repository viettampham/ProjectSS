using System;
using System.Collections.Generic;

namespace ProjectSS.Models.RequestModels
{
    public class CreateBillRequest
    {
        public Guid UserID { get; set; }
        public Guid CartID { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneNumberCustomer { get; set; }
        public string Address { get; set; }
    }
}