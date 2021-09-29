using System;

namespace AdvWorks.Models
{
    public class Person
    {
        public string PersonType { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Demographics{ get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }


    }
}
