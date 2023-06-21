using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class Employee
    {
        public int BusinessEntityID { get; set; }

        public String NationalIDNumber { get; set; }

        public String LoginID { get; set; }

        public String JobTitle { get; set; }

        public DateTime BirthDate { get; set; }

        public char MaritalStatus { get; set; }

        public char Gender { get; set; }

        public DateTime HireDate { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
