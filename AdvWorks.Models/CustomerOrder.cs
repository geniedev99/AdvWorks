using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class CustomerOrder
    {
        public int SalesOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int SalesPersonID { get; set; }
        public int Status { get; set; }
        public DateTime ShipDate { get; set; }

        public List<Product> Products { get; set; }
       // public Product product = new Product();
    }
}

