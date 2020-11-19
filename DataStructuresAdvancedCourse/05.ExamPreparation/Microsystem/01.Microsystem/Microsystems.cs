namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Microsystems : IMicrosystem
    {
        private readonly Dictionary<int, Computer> computers;
        private readonly Dictionary<Brand, OrderedBag<Computer>> computersByBrands;
        private readonly Dictionary<double, OrderedBag<Computer>> computersByScreenSize;
        private readonly Dictionary<string, OrderedBag<Computer>> computersByColors;
        private readonly OrderedDictionary<double, OrderedBag<Computer>> computersByPrice;

        public Microsystems()
        {
            this.computers = new Dictionary<int, Computer>();
            this.computersByBrands = new Dictionary<Brand, OrderedBag<Computer>>();
            this.computersByScreenSize = new Dictionary<double, OrderedBag<Computer>>();
            this.computersByColors = new Dictionary<string, OrderedBag<Computer>>();
            this.computersByPrice = new OrderedDictionary<double, OrderedBag<Computer>>();
        }

        public void CreateComputer(Computer computer)
        {
            if (this.computers.ContainsKey(computer.Number))
            {
                throw new ArgumentException();
            }

            this.computers.Add(computer.Number, computer);
            this.AddToComputersByBrands(computer);
            this.AddToComputersByScreenSize(computer);
            this.AddToComputersByColors(computer);
            this.AddToComputersByPrice(computer);
        }
       
        public bool Contains(int number)
        {
            return this.computers.ContainsKey(number);
        }

        public int Count()
        {
            return this.computers.Count;
        }

        public Computer GetComputer(int number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            return this.computers[number];
        }

        public void Remove(int number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            var computerToDelete = this.GetComputer(number);

            this.computersByBrands[computerToDelete.Brand].Remove(computerToDelete);
            this.computersByScreenSize[computerToDelete.ScreenSize].Remove(computerToDelete);
            this.computersByColors[computerToDelete.Color].Remove(computerToDelete);
            this.computersByPrice[computerToDelete.Price].Remove(computerToDelete);
            this.computers.Remove(number);
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!this.computersByBrands.ContainsKey(brand))
            {
                throw new ArgumentException();
            }

            var computersFromBrand = this.GetAllFromBrand(brand).Select(x => x.Number).ToList();

            foreach (var computer in computersFromBrand)
            {
                this.Remove(computer);
            }
        }

        public void UpgradeRam(int ram, int number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            var currentRam = this.computers[number].RAM;

            if (currentRam < ram)
            {
                this.computers[number].RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            if (!this.computersByBrands.ContainsKey(brand))
            {
                return Enumerable.Empty<Computer>();
            }

            return this.computersByBrands[brand];
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            if (!this.computersByScreenSize.ContainsKey(screenSize))
            {
                return Enumerable.Empty<Computer>();
            }

            return this.computersByScreenSize[screenSize];
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            if (!this.computersByColors.ContainsKey(color))
            {
                return Enumerable.Empty<Computer>();
            }

            return this.computersByColors[color];
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            var computersInRange = this.computersByPrice.Range(minPrice, true, maxPrice, true);
            var result = new OrderedBag<Computer>(new ComputerPriceDescComparer());

            foreach (var computers in computersInRange)
            {
                foreach (var computer in computers.Value)
                {
                    result.Add(computer);
                }
            }

            return result;
        }

        private void AddToComputersByBrands(Computer computer)
        {
            if (!this.computersByBrands.ContainsKey(computer.Brand))
            {
                this.computersByBrands[computer.Brand] = new OrderedBag<Computer>(new ComputerPriceDescComparer());
            }

            this.computersByBrands[computer.Brand].Add(computer);
        }

        private void AddToComputersByColors(Computer computer)
        {
            if (!this.computersByColors.ContainsKey(computer.Color))
            {
                this.computersByColors[computer.Color] = new OrderedBag<Computer>(new ComputerPriceDescComparer());
            }

            this.computersByColors[computer.Color].Add(computer);
        }

        private void AddToComputersByScreenSize(Computer computer)
        {
            if (!this.computersByScreenSize.ContainsKey(computer.ScreenSize))
            {
                this.computersByScreenSize[computer.ScreenSize] = new OrderedBag<Computer>();
            }

            this.computersByScreenSize[computer.ScreenSize].Add(computer);
        }

        private void AddToComputersByPrice(Computer computer)
        {
            if (!this.computersByPrice.ContainsKey(computer.Price))
            {
                this.computersByPrice[computer.Price] = new OrderedBag<Computer>(new ComputerPriceDescComparer());
            }

            this.computersByPrice[computer.Price].Add(computer);
        }
    }
}
