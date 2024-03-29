﻿using AdvWorks.Models;
using AdvWorks.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvWorks.Api.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

       
        [HttpGet]
        [Route("GetPersons")]
        public IActionResult GetPersons() 
        {
            try
            {
                List<Person> people = _personRepository.GetPeople();
                return Ok(people);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }


        [HttpPost]
        [Route("CreatePerson")]
        public IActionResult Create([FromBody] Person person)
        {
            try
            {
                bool result = _personRepository.CreatePerson(person);
                if (result)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }


        }

    }

    
}
