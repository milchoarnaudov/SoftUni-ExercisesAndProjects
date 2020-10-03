namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> _weapons;

        public Inventory()
        {
            this._weapons = new List<IWeapon>();
        }
        public int Capacity => this._weapons.Count;

        public void Add(IWeapon weapon)
        {
            this._weapons.Add(weapon);
        }

        public void Clear()
        {
            this._weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.GetById(weapon.Id) != null;
        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this._weapons.Count; i++)
            {
                if (this._weapons[i].Category == category)
                {
                    this._weapons[i].Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            IWeapon searchedWeapon = this.GetById(weapon.Id);

            if (searchedWeapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (searchedWeapon.Ammunition >= ammunition)
            {
                searchedWeapon.Ammunition -= ammunition;
                return true;
            }

            return false;
        }

        public IWeapon GetById(int id)
        {
            IWeapon weapon = null;

            for (int i = 0; i < this._weapons.Count; i++)
            {
                if (this._weapons[i].Id == id)
                {
                    weapon = this._weapons[i];
                    break;
                }
            }

            return weapon;
        }

        public IEnumerator GetEnumerator()
        {
            return this._weapons.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            IWeapon searchedWeapon = this.GetById(weapon.Id);

            if (searchedWeapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if ((searchedWeapon.Ammunition + ammunition) > searchedWeapon.MaxCapacity)
            {
                searchedWeapon.Ammunition = searchedWeapon.MaxCapacity;
            }
            else
            {
                searchedWeapon.Ammunition += ammunition;
            }

            return searchedWeapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            IWeapon weapon = this.GetById(id);

            if (weapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            this._weapons.Remove(weapon);

            return weapon;
        }

        public int RemoveHeavy()
        {
            int countOfRemovedHeavy = 0;

            this._weapons.RemoveAll(x =>
            {
                if (x.Category == Category.Heavy)
                {
                    countOfRemovedHeavy++;
                    return true;
                }
                return false;
            });

            return countOfRemovedHeavy;
        }

        public List<IWeapon> RetrieveAll()
        {
            return new List<IWeapon>(this._weapons);
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            List<IWeapon> result = new List<IWeapon>();

            int lowerBoundAsNum = (int)lower;
            int upperBoundAsNum = (int)upper;

            for (int i = 0; i < this._weapons.Count; i++)
            {
                IWeapon currentWeapon = this._weapons[i];
                int currentWeapontatusAsNum = (int)currentWeapon.Category;

                if (currentWeapontatusAsNum >= lowerBoundAsNum && currentWeapontatusAsNum <= upperBoundAsNum)
                {
                    result.Add(currentWeapon);
                }
            }

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int firstWeaponIndex = this._weapons.IndexOf(firstWeapon);
            this.ValidateIndex(firstWeaponIndex);

            int secondWeaponIndex = this._weapons.IndexOf(secondWeapon);
            this.ValidateIndex(secondWeaponIndex);

            if (firstWeapon.Category == secondWeapon.Category)
            {
                IWeapon temp = this._weapons[firstWeaponIndex];
                this._weapons[firstWeaponIndex] = this._weapons[secondWeaponIndex];
                this._weapons[secondWeaponIndex] = temp;
            }
        }

        private void ValidateIndex(int index)
        {
            if (index == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}
