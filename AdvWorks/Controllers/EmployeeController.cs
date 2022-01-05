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
                return BadRequest();
            }

        }
    }
}

      
