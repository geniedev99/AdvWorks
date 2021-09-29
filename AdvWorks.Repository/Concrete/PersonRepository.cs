using AdvWorks.Models;
using AdvWorks.Repository.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AdvWorks.Repository
{
    public class PersonRepository : IPersonRepository
    {

        public List<Person> GetPeople()
        {

            List<Person> personList;
            string sqlPersons = "SELECT [BusinessEntityID],[PersonType],[Title],[FirstName],[MiddleName],[LastName],[Demographics],[rowguid],[ModifiedDate] FROM [Person].[Person]";

            using (var connection = new SqlConnection("Server=tcp:geniedev99.database.windows.net,1433;Initial Catalog=AdventureWorks2019;Persist Security Info=False;User ID=geniedev99;Password=genieDev1!3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                personList = connection.Query<Person>(sqlPersons).ToList();
            }

            return personList;
        
        }
    }
}
