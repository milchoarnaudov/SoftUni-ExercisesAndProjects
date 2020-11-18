namespace Collection_of_Persons
{
    using System;

    public class Person : IComparable
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Town { get; set; }

        public int CompareTo(object other)
        {
            var otherPerson = other as Person;

            return this.Email.CompareTo(otherPerson.Email);
        }
    }
}
