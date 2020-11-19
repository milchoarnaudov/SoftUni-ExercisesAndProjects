namespace _01.Microsystem
{
    using System;

    public class Computer : IComparable
    {
        public Computer(int number, Brand brand, double price, double screenSize, string color)
        {
            this.Number = number;
            this.RAM = 8;
            this.Brand = brand;
            this.Price = price;
            this.ScreenSize = screenSize;
            this.Color = color;
        }
        public int Number { get; set; }

        public int RAM { get; set; }

        public Brand Brand { get; set; }

        public double Price { get; set; }

        public double ScreenSize { get; set; }

        public string Color { get; set; }

        public int CompareTo(object obj)
        {
            var otherComputer = obj as Computer;

            return otherComputer.Number.CompareTo(this.Number);
        }

        public override bool Equals(object obj)
        {
            var otherComputer = obj as Computer;

            if (otherComputer is null)
            {
                return false;
            }

            return this.Number == otherComputer.Number;
        }

        public override int GetHashCode()
        {
            return this.Number;
        }
    }
}
