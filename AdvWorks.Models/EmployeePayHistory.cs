using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
   public  class EmployeePayHistory
    {

        public int BusinessEntityId{ get; set; }

        public DateTime RateChangeDate{ get; set; }

        public DateTime ModifiedDate { get; set; }

        public decimal Rate{ get; set; }

        public int PayFrequency { get; set; }

    }
}
