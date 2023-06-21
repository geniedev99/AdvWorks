using AdvWorks.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Repository.Interfaces
{
    public interface IProductRepository
    {
        public List<Product> GetProducts();

        public ProductDetails GetProductDetails(int ProductID);
        public bool CreateProduct(Product product);
    }
}
