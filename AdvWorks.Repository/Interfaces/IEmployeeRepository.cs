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

        }
    
}
