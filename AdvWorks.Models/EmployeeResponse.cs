using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class EmployeeResponse
    {
        public int BusinessEntityID { get; set; }

        public String NationalIDNumber { get; set; }

        public String LoginID { get; set; }

        public String JobTitle { get; set; }
    }
}
