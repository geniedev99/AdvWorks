using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class ProductSubCategory
    {
        public int ProductSubcategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }

    }
}
