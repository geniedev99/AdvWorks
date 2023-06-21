using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EmpSalesOrder
    {
        [JsonProperty]
        public EmployeeResponse EmployeeResponse = new EmployeeResponse();
        [JsonProperty]
        public List<SalesOrder> Orders = new List<SalesOrder>();
    }
}
