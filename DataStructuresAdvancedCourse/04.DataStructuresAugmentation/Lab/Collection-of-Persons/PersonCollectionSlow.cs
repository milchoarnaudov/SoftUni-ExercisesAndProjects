namespace Collection_of_Persons
{
    using System.Collections.Generic;
    using System.Linq;

    public class PersonCollectionSlow : IPersonCollection
    {
        private readonly List<Person> persons;

        public PersonCollectionSlow()
        {
            this.persons = new List<Person>();
        }

        public bool AddPerson(string email, string name, int age, string town)
        {
            if (this.persons.Any(x => x.Email == email))
            {
                return false;
            }

            var person = new Person
            {
                Email = email,
                Name = name,
                Age = age,
                Town = town
            };

            this.persons.Add(person);

            return true;
        }

        public int Count => this.persons.Count;

        public Person FindPerson(string email)
        {
            return this.persons.FirstOrDefault(x => x.Email == email);
        }

        public bool DeletePerson(string email)
        {
            var person = this.FindPerson(email);

            if (person == null)
            {
                return false;
            }

            this.persons.Remove(person);

            return true;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            return this.persons.Where(x => x.Email.EndsWith("@" + emailDomain))
                .OrderBy(x => x.Email);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            return this.persons.Where(x => x.Name == name && x.Town == town)
                .OrderBy(x => x.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            return this.persons.Where(x => x.Age >= startAge && x.Age <= endAge)
                .OrderBy(x => x.Age)
                .ThenBy(x => x.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            return this.persons.Where(x => x.Age >= startAge && x.Age <= endAge && x.Town == town)
                .OrderBy(x => x.Age)
                .ThenBy(x => x.Email);
        }
    }
}
