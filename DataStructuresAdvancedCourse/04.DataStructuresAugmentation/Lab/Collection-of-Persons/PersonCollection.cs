namespace Collection_of_Persons
{
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class PersonCollection : IPersonCollection
    {
        private readonly Dictionary<string, Person> persons;
        private readonly Dictionary<string, SortedSet<Person>> personsByEmailDomains;
        private readonly Dictionary<string, SortedSet<Person>> personsByNameTown;
        private readonly OrderedDictionary<int, SortedSet<Person>> personsByAge;
        private readonly Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> personsByAgeTown;

        public PersonCollection()
        {
            this.persons = new Dictionary<string, Person>();
            this.personsByEmailDomains = new Dictionary<string, SortedSet<Person>>();
            this.personsByNameTown = new Dictionary<string, SortedSet<Person>>();
            this.personsByAge = new OrderedDictionary<int, SortedSet<Person>>();
            this.personsByAgeTown = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
        }

        public bool AddPerson(string email, string name, int age, string town)
        {
            if (this.persons.ContainsKey(email))
            {
                return false;
            }

            var emailDomain = email.Split('@')[1];
            var person = new Person
            {
                Email = email,
                Name = name,
                Age = age,
                Town = town
            };
            var nameAndTown = this.CombineNameAndTown(person.Name, person.Town);

            this.persons.Add(email, person);
            this.personsByEmailDomains.AppendValueToKey(emailDomain, person);
            this.personsByNameTown.AppendValueToKey(nameAndTown, person);
            this.personsByAge.AppendValueToKey(age, person);
            this.personsByAgeTown.EnsureKeyExists(town);
            this.personsByAgeTown[town].AppendValueToKey(age, person);

            return true;
        }

        public int Count => this.persons.Count;

        public Person FindPerson(string email)
        {
            if (!this.persons.ContainsKey(email))
            {
                return null;
            }

            return this.persons[email];
        }

        public bool DeletePerson(string email)
        {
            var person = this.FindPerson(email);

            if (person == null)
            {
                return false;
            }

            var nameAndTown = this.CombineNameAndTown(person.Name, person.Town);
            var emailDomain = email.Split('@')[1];
            var result = this.persons.Remove(email);

            this.personsByNameTown[nameAndTown].Remove(person);
            this.personsByEmailDomains[emailDomain].Remove(person);
            this.personsByAge[person.Age].Remove(person);
            this.personsByAgeTown[person.Town][person.Age].Remove(person);

            return result;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            return this.personsByEmailDomains.GetValuesForKey(emailDomain);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            var nameAndTown = this.CombineNameAndTown(name, town);

            return this.personsByNameTown.GetValuesForKey(nameAndTown);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            var personsInAgeRange = this.personsByAge.Range(startAge, true, endAge, true);

            foreach (var personList in personsInAgeRange)
            {
                foreach (var person in personList.Value)
                {
                    yield return person;
                }
            }
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            if (!this.personsByAgeTown.ContainsKey(town))
            {
                yield break;
            }

            var personsInAgeRange = this.personsByAgeTown[town].Range(startAge, true, endAge, true);

            foreach (var personList in personsInAgeRange)
            {
                foreach (var person in personList.Value)
                {
                    yield return person;
                }
            }
        }

        private string CombineNameAndTown(string name, string town)
        {
            var separator = "|!|";

            return name + separator + town;
        }
    }
}
