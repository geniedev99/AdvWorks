using AdvWorks.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace AdvWorks.Repository.Interfaces
{
   public interface IPersonRepository
    {
        public List<Person> GetPeople();
        public bool CreatePerson(Person person);
    }
}
