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
        private readonly string connString;

        public Guid MyGuid { get; set; }

        public string MyGuidString
        {
            get { return MyGuid.ToString(); }
            set { MyGuid = new Guid(value); }
        }

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connString = _configuration.GetConnectionString("AdvWorks");
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

        public ProductDetails GetProductDetails(int ProductID)
        {
            ProductDetails details = new ProductDetails();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("select p.ProductID,p.ProductNumber,p.Name,p.ListPrice from Production.Product p " +
 " where p.ProductID = " + ProductID, connection);
                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Product product = new Product();

                            details.Product.ProductId = Convert.ToInt32(sdr["ProductId"]);
                            details.Product.ProductNumber = Convert.ToString(sdr["ProductNumber"]);
                            details.Product.Name = Convert.ToString(sdr["Name"]);
                            details.Product.ListPrice = Convert.ToDecimal(sdr["ListPrice"]);
                        }
                    }

                    sdr.Close();

                    SqlCommand command1 = new SqlCommand("select h.ProductID,h.StartDate,h.EndDate,h.ListPrice from Production.ProductListPriceHistory h where h.ProductID = " + ProductID, connection);
                    //connection.Open();
                    SqlDataReader sdr1 = command1.ExecuteReader();
                    if (sdr1.HasRows)
                    {
                        while (sdr1.Read())
                        {

                            ProductListPriceHistory History = new ProductListPriceHistory();

                            History.ProductID = Convert.ToInt32(sdr1["ProductId"]);
                            History.StartDate = Convert.ToDateTime(sdr1["StartDate"]);
                            History.EndDate = sdr1["EndDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(sdr1["EndDate"]);
                            History.ListPrice = sdr1["ListPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(sdr1["ListPrice"]);

                            details.Product.ProductListPriceHistories.Add(History);
                        }
                    }
                    sdr1.Close();

                    SqlCommand command2 = new SqlCommand("select  psc.ProductSubcategoryID,psc.ProductCategoryID,psc.Name as SubCategoryName ,pc.Name as CategoryName " +
" from Production.ProductCategory pc , Production.ProductSubcategory psc, Production.Product p  where " +
" pc.ProductCategoryID = psc.ProductCategoryID and p.ProductSubcategoryID = psc.ProductSubcategoryID and p.ProductID = " + ProductID, connection);

                    SqlDataReader sdr2 = command2.ExecuteReader();

                    if (sdr2.HasRows)
                    {
                        while (sdr2.Read())
                        {
                            details.Product.SubCategory.ProductSubcategoryID = Convert.ToInt32(sdr2["ProductSubcategoryID"]);
                            details.Product.SubCategory.SubCategoryName = Convert.ToString(sdr2["SubCategoryName"]);
                            details.Product.SubCategory.ProductCategoryID = Convert.ToInt32(sdr2["ProductCategoryID"]);
                            details.Product.SubCategory.CategoryName = Convert.ToString(sdr2["CategoryName"]);

                        }

                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }

            return details;
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
