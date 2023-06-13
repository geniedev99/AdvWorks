using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class Vendor
    {
        public int BusinessEntityID { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public int CreditRating { get; set; }
        public bool PreferredVendorStatus { get; set; }
        public bool ActiveFlag { get; set; }
        public string PurchasingWebServiceURL { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
