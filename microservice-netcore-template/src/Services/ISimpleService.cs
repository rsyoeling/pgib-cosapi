using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Services
{
    public interface ISimpleService
    {
        public IEnumerable<Person> findAllPersons();
        public void createPerson(Person person);
    }

}
