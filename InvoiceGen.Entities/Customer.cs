using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.Entities
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public CustomerType CustomerType { get; set; }
        public string ContactName { get; set; }
        public string MobileNumber { get; set; }
        public string CustomerGSTIN { get; set; }
        public string PAN { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
    }


    public class CustomerType
    {
        public const string Individual = "Individual";
        public const string Company = "Company";
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }


}

