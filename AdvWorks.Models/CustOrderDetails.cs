using System;
using System.Collections.Generic;
using System.Text;

namespace AdvWorks.Models
{
    public class CustOrderDetails
    {
      public  CustomerDetails CustomerDetails = new CustomerDetails();
      public  List<CustomerOrder> CustomerOrders = new List<CustomerOrder>();
    }
}
