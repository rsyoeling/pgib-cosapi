using Api.Entities;
using System.Collections.Generic;
using Common.Attributes;

namespace Api.Services.Impl
{
    [Service(Scope="Transient")]
    public class SimpleService : ISimpleService
    {

        private List<Person> persons = new List<Person>();

        public SimpleService()
        {
        }

        public IEnumerable<Person> findAllPersons()
        {
            if (this.persons.Count == 0)
            {
                this.persons.Add(new Person("Jane"));
                this.persons.Add(new Person("Kurt"));
                this.persons.Add(new Person("Patterson"));
            }
            return this.persons;
        }

        public void createPerson(Person person)
        {
            persons.Add(person);
        }

    }
}
