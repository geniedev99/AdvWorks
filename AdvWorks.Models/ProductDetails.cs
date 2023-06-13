using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class ProductDetails
    {

        public ProductDetails()
        {
            Product = new Product();
        }
       public Product Product { get; set; }
     }
}
