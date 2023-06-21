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
    public class PersonRepository :IPersonRepository
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


        public bool CreatePerson(Person person) 
        {
            string insertSql = @"INSERT INTO Person.Person( BusinessEntityID,PersonType,NameStyle,Title ,FirstName,MiddleName,LastName, EmailPromotion , rowguid ,ModifiedDate )
                                 values ( @BusinessEntityID,@PersonType, @NameStyle, @Title , @FirstName, @MiddleName, @LastName, @EmailPromotion, @rowguid , @ModifiedDate)";

            var guid = Guid.NewGuid();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("AdvWorks")))
                {
                    connection.Execute(insertSql, new
                    {
                        @BusinessEntityID = person.BusinessEntityID,
                        @PersonType = person.PersonType,
                        @NameStyle = person.NameStyle,
                        @Title = person.Title,
                        @FirstName = person.FirstName,
                        @MiddleName = person.MiddleName,    
                        @LastName = person.LastName,
                        @EmailPromotion = person.EmailPromotion,
                        @rowguid = guid,
                        @ModifiedDate = DateTime.Now

                    });
                    return true;
                }
            }


            catch (Exception ex)
            {
                return false;
            }




        }
    }
}
