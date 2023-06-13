using AdvWorks.Models;
using AdvWorks.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;



namespace AdvWorks.Repository.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connString = _configuration.GetConnectionString("AdvWorks"); 
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> empList = new List<Employee>();
            string sqlemp = "SELECT BusinessEntityID,NationalIDNumber,LoginID,JobTitle,BirthDate, MaritalStatus,Gender,HireDate,rowguid,ModifiedDate FROM HumanResources.Employee";

            using (var connection = new SqlConnection(connString))
            {
                empList = connection.Query<Employee>(sqlemp).ToList();
            }

            return empList;
        }


        public List<EmployeePayHistory> GetEmployeePayHistories()
        {
            List<EmployeePayHistory> employeePayHistories = new List<EmployeePayHistory>();

            string sqlemp = "SELECT BusinessEntityID,RateChangeDate,Rate,PayFrequency,ModifiedDate FROM HumanResources.EmployeePayHistory";

            using (var connection = new SqlConnection(connString))
            {
                employeePayHistories = connection.Query<EmployeePayHistory>(sqlemp).ToList();
            }

            return employeePayHistories;
        }

        public List<SalesOrder> GetSalesOrders()
        {
            List<SalesOrder> sales = new List<SalesOrder>();
            string sqlorder = "select SalesOrderID,OrderDate,SalesOrderNumber,TerritoryID  from Sales.SalesOrderHeader";
            using (var connection = new SqlConnection(connString))
            {
                sales = connection.Query<SalesOrder>(sqlorder).ToList();
            }
            return sales;
        }

        public List<Vendor> GetVendors()
        {
            List<Vendor> Vendors = new List<Vendor>();
            string sqlvendor = "select * from Purchasing.Vendor";

            using (var connection = new SqlConnection(connString))
            {
                Vendors = connection.Query<Vendor>(sqlvendor).ToList();
            }
            return Vendors;
        }
        public CustomerDetails GetCustomerDetails(int CustomerID)
        {
            CustomerDetails customerDetails = new CustomerDetails();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("select c.CustomerID, p.FirstName + ' ' + p.LastName AS FullName , c.AccountNumber," +
"p.PersonType,pp.PhoneNumber,p.Demographics from Sales.Customer c, Person.Person p, Person.PersonPhone pp " +
" where c.PersonID = p.BusinessEntityID and p.BusinessEntityID = pp.BusinessEntityID and c.CustomerID = " + CustomerID, connection);

                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            customerDetails.CustomerID = Convert.ToInt32(sdr["CustomerID"]);
                            customerDetails.FullName = Convert.ToString(sdr["FullName"]);
                            customerDetails.AccountNumber = Convert.ToString(sdr["AccountNumber"]);
                            customerDetails.PersonType = Convert.ToString(sdr["PersonType"]);
                            customerDetails.PhoneNumber = Convert.ToString(sdr["PhoneNumber"]);
                            customerDetails.Demographics = Convert.ToString(sdr["Demographics"]);
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
            }

            return customerDetails;
        }

        public List<CustomerOrder> GetCustomerOrders(int CustomerID)
        {
            List<CustomerOrder> customerOrders = new List<CustomerOrder>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("select soh.SalesOrderID,soh.OrderDate,soh.SalesPersonID,soh.Status,soh.ShipDate,sod.ProductID,p.Name,p.ProductNumber from "+
" Sales.SalesOrderHeader soh, Sales.SalesOrderDetail sod, Production.Product p, Sales.Customer c where "+
 " c.CustomerID = soh.CustomerID and soh.SalesOrderID = sod.SalesOrderID and sod.ProductID = p.ProductID and c.CustomerID = "+ CustomerID, connection);
                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Product product = new Product();
                            product.ProductId = Convert.ToInt32(sdr["ProductId"]);
                            product.Name = Convert.ToString(sdr["Name"]);
                            product.ProductNumber = Convert.ToString(sdr["ProductNumber"]);

                            //new order
                            if (!customerOrders.Select(x=>x.SalesOrderID).Contains(Convert.ToInt32(sdr["SalesOrderID"])))
                            {
                                CustomerOrder newOrder = new CustomerOrder();
                                newOrder.SalesOrderID = Convert.ToInt32(sdr["SalesOrderID"]);
                                newOrder.OrderDate = Convert.ToDateTime(sdr["OrderDate"]);
                                newOrder.SalesPersonID = sdr["SalesPersonID"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["SalesPersonID"]);
                                newOrder.Status = Convert.ToInt32(sdr["Status"]);
                                newOrder.ShipDate = Convert.ToDateTime(sdr["OrderDate"]);
                                newOrder.Products = new List<Product>();
                                newOrder.Products.Add(product);
                                customerOrders.Add(newOrder);
                            }
                            //existing order
                            else
                            {
                                var existingOrder = customerOrders.Where(x => x.SalesOrderID == Convert.ToInt32(sdr["SalesOrderID"])).FirstOrDefault();
                                existingOrder.Products.Add(product);
                            }
                            
                        }
                    }
                   // var cust = customerOrders.Select(x => x.SalesOrderID).Distinct();
                    //foreach (var item in customerOrders)
                    //{
                        
                    //}


                    connection.Close();
                }

            }
            catch (Exception e)
            {
            }

            return customerOrders;
        }
        public List<Product> GetProductDetails(int CustomerID)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("select sod.ProductID,p.Name,p.ProductNumber " +
                        "from Sales.SalesOrderHeader soh,Sales.SalesOrderDetail sod, Production.Product p " +
                 "where soh.SalesOrderID = sod.SalesOrderID and sod.ProductID = p.ProductID and soh.CustomerID = " + CustomerID, connection);
                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Product product = new Product();
                            product.ProductId = Convert.ToInt32(sdr["ProductId"]);
                            product.Name = Convert.ToString(sdr["Name"]);
                            product.ProductNumber = Convert.ToString(sdr["ProductNumber"]);
                            products.Add(product);
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {

            }
            return products;
        }

//        public List<CustOrderDetails> GetCustOrderDetails(int CustomerID)
//        {
//            List<CustOrderDetails> custOrderDetails = new List<CustOrderDetails>();
//            CustOrderDetails details = new CustOrderDetails();
//            try
//            {
//                using (SqlConnection connection = new SqlConnection(connString))
//                {
//                    SqlCommand command = new SqlCommand("select c.CustomerID, p.FirstName + ' ' + p.LastName AS FullName , c.AccountNumber," +
//"p.PersonType, pp.PhoneNumber, p.Demographics, soh.SalesOrderID, soh.OrderDate, soh.SalesPersonID, soh.Status, soh.ShipDate," +
//" sod.ProductID, pr.Name, pr.ProductNumber from Sales.Customer c, Person.Person p, Person.PersonPhone  pp, Sales.SalesOrderHeader soh," +
//" Sales.SalesOrderDetail sod, Production.Product pr where c.PersonID = p.BusinessEntityID and p.BusinessEntityID = pp.BusinessEntityID " +
//" and  c.CustomerID = soh.CustomerID and  soh.SalesOrderID = sod.SalesOrderID and sod.ProductID = pr.ProductID and  c.CustomerID = " + CustomerID, connection);
//                    connection.Open();
//                    SqlDataReader sdr = command.ExecuteReader();

//                    if (sdr.HasRows)
//                    {
//                        while (sdr.Read())
//                        {
//                            details.CustomerDetails.CustomerID = Convert.ToInt32(sdr["CustomerID"]);
//                            details.CustomerDetails.FullName = Convert.ToString(sdr["FullName"]);
//                            details.CustomerDetails.AccountNumber = Convert.ToString(sdr["AccountNumber"]);
//                            details.CustomerDetails.PersonType = Convert.ToString(sdr["PersonType"]);
//                            details.CustomerDetails.PhoneNumber = Convert.ToString(sdr["PhoneNumber"]);
//                            details.CustomerDetails.Demographics = Convert.ToString(sdr["Demographics"]);

//                            if (!details.CustomerOrders.Select(x=>x.SalesOrderID).Contains(Convert.ToInt32(sdr["SalesOrderID"])))
//                            {
//                                CustomerOrder order = new CustomerOrder();

//                                order.SalesOrderID = Convert.ToInt32(sdr["SalesOrderID"]);
//                                order.OrderDate = Convert.ToDateTime(sdr["OrderDate"]);
//                                order.SalesPersonID = sdr["SalesPersonID"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["SalesPersonID"]);
//                                order.Status = Convert.ToInt32(sdr["Status"]);
//                                order.ShipDate = Convert.ToDateTime(sdr["OrderDate"]);

//                                details.CustomerOrders.Add(order);
//                            }
                            
//                            //foreach (var order in details.CustomerOrders)
//                            //{
//                            //    Product product = new Product();

//                            //    product.ProductId = Convert.ToInt32(sdr["ProductId"]);
//                            //    product.Name = Convert.ToString(sdr["Name"]);
//                            //    product.ProductNumber = Convert.ToString(sdr["ProductNumber"]);

//                            //    order.Products.Add(product);

//                            //}


//                            //custOrderDetails.Add(details);

//                        }
//                        connection.Close();
//                    }

//                }
//            }
//            catch (Exception e)
//            {
//            }

//            return custOrderDetails;
//        }


   
        public EmpSalesOrder GetEmpSalesOrders(String NationalIDNumber)
        {
            EmpSalesOrder emp = new EmpSalesOrder();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("select emp.BusinessEntityID,emp.NationalIDNumber, emp.LoginID,emp.JobTitle," +
    "soh.SalesOrderID,soh.OrderDate, soh.SalesOrderNumber,soh.TerritoryID from " +
    "HumanResources.Employee emp, Sales.SalesPerson sp, Sales.SalesOrderHeader soh " +
    "where emp.BusinessEntityID = sp.BusinessEntityID and sp.BusinessEntityID = soh.SalesPersonID and emp.NationalIDNumber= " + NationalIDNumber, connection);
                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            emp.EmployeeResponse.BusinessEntityID = Convert.ToInt32(sdr["BusinessEntityID"]);
                            emp.EmployeeResponse.NationalIDNumber = Convert.ToString(sdr["NationalIDNumber"]);
                            emp.EmployeeResponse.LoginID = Convert.ToString(sdr["LoginID"]);
                            emp.EmployeeResponse.JobTitle = Convert.ToString(sdr["JobTitle"]);

                            SalesOrder order = new SalesOrder();


                            order.SalesOrderID = Convert.ToInt32(sdr["SalesOrderID"]);
                            order.OrderDate = Convert.ToDateTime(sdr["OrderDate"]);
                            order.SalesOrderNumber = Convert.ToString(sdr["SalesOrderNumber"]);
                            order.TerritoryID = Convert.ToInt32(sdr["TerritoryID"]);


                            emp.Orders.Add(order);
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
            }

            return emp;
        }



    }
}
