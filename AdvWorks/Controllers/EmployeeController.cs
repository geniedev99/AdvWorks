using AdvWorks.Models;
using AdvWorks.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvWorks.Api.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _empRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _empRepository = employeeRepository;
        }


        [HttpGet]
        [Route("GetEmployees")]
        public IActionResult GetEmployees()
        {
            try
            {
                List<Employee> employees = _empRepository.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        //get even employees
        [HttpGet]
        [Route("GetEvenEmployees")]
        public IActionResult GetEvenEmployees()
        {
            try
            {
                List<Employee> employees = _empRepository.GetEmployees();

                List<Employee> employees1 = new List<Employee>();

                Employee emp = new Employee();
                foreach (var item in employees)
                {

                    if (item.BusinessEntityID % 2 == 0)
                    {
                        emp = item;
                        employees1.Add(emp);
                    }

                }
                return Ok(employees1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }
        [HttpGet]
        [Route("GetEmployeeResponse")]
        public IActionResult GetEmployeeResponse()
        {
            try
            {
                List<Employee> employees = _empRepository.GetEmployees();
                List<EmployeeResponse> employees1 = new List<EmployeeResponse>();
                EmployeeResponse emp = new EmployeeResponse();

                foreach (var item in employees)
                {
                    emp.BusinessEntityID = item.BusinessEntityID;
                    emp.NationalIDNumber = item.NationalIDNumber;
                    emp.LoginID = item.LoginID;
                    emp.JobTitle = item.JobTitle;
                    employees1.Add(emp);
                }
                return Ok(employees1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }
        [HttpGet]
        [Route("GetEmployeeResponseByID")]
        public IActionResult GetEmployeeResponseByID([FromQuery] string nationalIdNumber)
        {
            try
            {
                List<Employee> employees = _empRepository.GetEmployees();
                EmployeeResponse emp = new EmployeeResponse();
                foreach (var item in employees)
                {
                    if (item.NationalIDNumber == nationalIdNumber)
                    {
                        emp.BusinessEntityID = item.BusinessEntityID;
                        emp.NationalIDNumber = item.NationalIDNumber;
                        emp.LoginID = item.LoginID;
                        emp.JobTitle = item.JobTitle;
                    }

                }
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }


        [HttpGet]
        [Route("GetEmpSalesOrders")]
        public IActionResult GetEmpSalesOrders([FromQuery] string nationalIdNumber)
        {
            try
            {
                EmpSalesOrder emp = _empRepository.GetEmpSalesOrders(nationalIdNumber);

                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }


        [HttpGet]
        [Route("GetEmployeePayHistories")]
        public IActionResult GetEmployeePayHistories()
        {
            try
            {
                List<EmployeePayHistory> employeePayHistories = _empRepository.GetEmployeePayHistories();
                return Ok(employeePayHistories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }
        [HttpGet]
        [Route("GetVendors")]
        public IActionResult GetVendors()
        {
            try
            {
                List<Vendor> vendors = _empRepository.GetVendors();
                return Ok(vendors);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }

        }
        [HttpGet]
        [Route("GetCustomerDetails")]
        public IActionResult GetCustomerDetails([FromQuery] int CustomerID)
        {
            try
            {
                CustomerDetails customerDetails = _empRepository.GetCustomerDetails(CustomerID);
                return Ok(customerDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }

        }
        [HttpGet]
        [Route("GetCustomerOrders")]
        public IActionResult GetCustomerOrders([FromQuery] int CustomerID)
        {
            try
            {
                List<CustomerOrder> customerOrders = _empRepository.GetCustomerOrders(CustomerID);
                return Ok(customerOrders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }
        [HttpGet]
        [Route("GetProductDetails")]
        public IActionResult GetProductDetails([FromQuery] int CustomerID)
        {
            try
            {
                List<Product> products = _empRepository.GetProductDetails(CustomerID);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        //[HttpGet]
        //[Route("GetCustOrderDetails")]
        //public IActionResult GetCustOrderDetails([FromQuery] int CustomerID)
        //{
        //    try
        //    {
        //        CustOrderDetails custOrderDetails = new CustOrderDetails();

        //        custOrderDetails.CustomerDetails = _empRepository.GetCustomerDetails(CustomerID);
        //        custOrderDetails.CustomerOrders = _empRepository.GetCustomerOrders(CustomerID);
                
        //        //List<Product> Allproducts = _empRepository.GetProductDetails(CustomerID);

        //        foreach (var order in custOrderDetails.CustomerOrders)
        //        {
        //            var products = _empRepository.GetProductsForOrder(order.SalesOrderID);
        //            order.Products.AddRange(products);
        //        }


        //        return Ok(custOrderDetails);


        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.StackTrace);
        //    }


        //}

        [HttpGet]
        [Route("GetEmployeePayHistoryById")]
        public IActionResult GetEmployeePayHistoryById([FromQuery] string nationalIdNumber)
        {
            try
            {
                List<Employee> employees = _empRepository.GetEmployees();
                List<EmployeePayHistory> employeePayHistories = _empRepository.GetEmployeePayHistories();

                var employee = employees.Where(e => e.NationalIDNumber == nationalIdNumber).FirstOrDefault();
                var filteredEmployeePayHistories = employeePayHistories.Where(e => e.BusinessEntityId == employee.BusinessEntityID);

                return Ok(filteredEmployeePayHistories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("GetEmployeePayHistoryById_woLinq")]
        public IActionResult GetEmployeePayHistoryById_woLinq([FromQuery] string nationalIdNumber)
        {
            try
            {
                List<Employee> employees = _empRepository.GetEmployees();
                List<EmployeePayHistory> employeePayHistories = _empRepository.GetEmployeePayHistories();

                var employee = employees.Where(e => e.NationalIDNumber == nationalIdNumber).FirstOrDefault();
                var filteredEmployeePayHistories = employeePayHistories.Where(e => e.BusinessEntityId == employee.BusinessEntityID);

                return Ok(filteredEmployeePayHistories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }
    }
}
#region commented for
//for (int i = 0; i<employees.Count; i++)
//            {
//                if(employees[i].NationalIDNumber.ToLower() == nationalIdNumber.ToLower())
//                {
//                    businessEntId = employees[i].BusinessEntityID;
//                    break;
//                }
//            }

//            for (int j = 0; j < employeePayHistories.Count; j++)
//{
//    if (employeePayHistories[j].BusinessEntityId == businessEntId)
//    {
//        filteredEmployeePayHistories.Add(employeePayHistories[j]);
//    }
//}
#endregion

#region commented foreach
//    foreach (Employee emp in employees)
//                {
//                    if(emp.NationalIDNumber == nationalIdNumber)
//                    {
//                        businessEntId = emp.BusinessEntityID;
//                        break;
//                    }
//                }

//                foreach (EmployeePayHistory emppay in employeePayHistories)
//{
//    if (emppay.BusinessEntityId == businessEntId)
//    {
//        filteredEmployeePayHistories.Add(emppay);
//    }

//}
#endregion















