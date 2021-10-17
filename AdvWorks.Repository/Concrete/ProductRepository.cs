using AdvWorks.Models;
using AdvWorks.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AdvWorks.Repository.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public List<Product> GetProducts()
        {
            List<Product> productList;
            string sqlProducts = "SELECT ProductId,ProductNumber,Name ,MakeFlag,FinishedGoodsFlag ,Color,SafetyStockLevel ,ListPrice ,Size ,SellStartDate,SellEndDate FROM Production.Product";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("AdvWorks")))
            {
                productList = connection.Query<Product>(sqlProducts).ToList();
            }

            return productList;


        }
    }
}
