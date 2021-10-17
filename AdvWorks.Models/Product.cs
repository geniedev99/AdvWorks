using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductNumber { get; set; }
        public string Name { get; set; }
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public int SafetyStockLevel { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public DateTime SellStartDate { get; set; }
        public DateTime SellEndDate { get; set; }

    }
}
