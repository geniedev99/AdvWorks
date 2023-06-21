using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class ProductListPriceHistory
    {
        public int ProductID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ListPrice { get; set; }
        
      

    }
}
