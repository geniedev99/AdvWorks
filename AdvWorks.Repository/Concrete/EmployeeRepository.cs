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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connString = _configuration.GetConnectionString("AdvWorks"); ;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> empList;
            string sqlemp = "SELECT BusinessEntityID,NationalIDNumber,LoginID,JobTitle,BirthDate, MaritalStatus,Gender,HireDate,rowguid,ModifiedDate FROM HumanResources.Employee";

            using (var connection = new SqlConnection(connString))
            {
                empList = connection.Query<Employee>(sqlemp).ToList();
            }

            return empList;
        }
    }
}
