using AdvWorks.Models;
using AdvWorks.Repository.Interfaces;
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
        [Route("GetEmployeePayHistoryById")]
        public IActionResult GetEmployeePayHistoryById([FromQuery]string nationalIdNumber)
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


}

 
 
 
 
 
 
 
 


