using System.Data;

namespace SUS.HTTP
{
    public class Cookie
    {
        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public Cookie(string cookieAsString)
        {
            var cookiesParts = cookieAsString.Split(new char[] { '=' }, 2);
            this.Name = cookiesParts[0];
            this.Value = cookiesParts[1];
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}