using AdvWorks.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Repository.Interfaces
{


    public interface IEmployeeRepository
    {
        public List<Employee> GetEmployees();
        public List<EmployeePayHistory> GetEmployeePayHistories();

        public EmpSalesOrder GetEmpSalesOrders(String NationalIDNumber);
        public List<Vendor> GetVendors();
        public CustomerDetails GetCustomerDetails(int CustomerID);
        public List<CustomerOrder> GetCustomerOrders(int CustomerID);
        public List<Product> GetProductDetails(int CustomerID);
        //public List<CustOrderDetails> GetCustOrderDetails(int CustomerID);



    }

}
