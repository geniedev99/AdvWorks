using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class SalesOrder
    {
        public int SalesOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string SalesOrderNumber { get; set; }
        public int TerritoryID { get; set; }
    }
}
