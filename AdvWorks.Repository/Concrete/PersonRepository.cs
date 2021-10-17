using AdvWorks.Models;
using AdvWorks.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AdvWorks.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IConfiguration _configuration;
        public PersonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Person> GetPeople()
        {

            List<Person> personList;
            string sqlPersons = "SELECT [BusinessEntityID],[PersonType],[Title],[FirstName],[MiddleName],[LastName],[Demographics],[rowguid],[ModifiedDate] FROM [Person].[Person]";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("AdvWorks")))
            {
                personList = connection.Query<Person>(sqlPersons).ToList();
            }

            return personList;
        
        }
    }
}
