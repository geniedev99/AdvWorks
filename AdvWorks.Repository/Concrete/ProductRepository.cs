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

        public Guid MyGuid { get; set; }

        public string MyGuidString
        {
            get { return MyGuid.ToString(); }
            set { MyGuid = new Guid(value); }
        }

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

        public bool CreateProduct(Product product)
        {

            string insertSql = @"INSERT INTO Production.Product
                                       (Name
                                       ,ProductNumber
                                       ,MakeFlag
                                       ,FinishedGoodsFlag
                                       ,SafetyStockLevel
                                       ,ReorderPoint
                                       ,StandardCost
                                       ,ListPrice
                                       ,DaysToManufacture
                                       ,SellStartDate
                                       ,rowguid
                                       ,ModifiedDate)
                                        VALUES
                                        (
                                            @Name
                                           ,@ProductNumber
                                           ,@MakeFlag
                                           ,@FinishedGoodsFlag
                                           ,@SafetyStockLevel
                                           ,@ReorderPoint
                                           ,@StandardCost
                                           ,@ListPrice
                                           ,@DaysToManufacture
                                           ,@SellStartDate
                                           ,@rowguid
                                           ,@ModifiedDate
                                        )";

            var guid = Guid.NewGuid();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("AdvWorks")))
                {
                    connection.Execute(insertSql, new
                    {
                        @name = product.Name,
                        @ProductNumber = product.ProductNumber,
                        @MakeFlag = product.MakeFlag,
                        @FinishedGoodsFlag = product.FinishedGoodsFlag,
                        @SafetyStockLevel = product.SafetyStockLevel,
                        @ReorderPoint = product.ReorderPoint,
                        @StandardCost = product.StandardCost,
                        @ListPrice = product.ListPrice,
                        @DaysToManufacture = product.DaysToManufacture,
                        @SellStartDate = product.SellStartDate,
                        @rowguid = guid,
                        @ModifiedDate = DateTime.Now
                    });
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            

            
        }
    }
}
