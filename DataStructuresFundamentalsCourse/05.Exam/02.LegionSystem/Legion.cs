namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> _enemies;

        public Legion()
        {
            this._enemies = new OrderedSet<IEnemy>();
        }

        public int Size => _enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            return this._enemies.TryGetItem(enemy, out _);
        }

        public void Create(IEnemy enemy)
        {
            this._enemies.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            if (speed < 0 || speed >= this.Size)
            {
                return null;
            }

            return this._enemies[this.Size - 1 - speed];
        }

        public List<IEnemy> GetFaster(int speed)
        {
            List<IEnemy> fasterEnemies = new List<IEnemy>();

            for (int i = 0; i < this._enemies.Count; i++)
            {
                if (this._enemies[i].AttackSpeed > speed)
                {
                    fasterEnemies.Add(this._enemies[i]);
                }
            }

            return fasterEnemies;
        }

        public IEnemy GetFastest()
        {
            this.EnsureNotEmtpy();

            return this._enemies.GetFirst();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            List<IEnemy> result = new List<IEnemy>(this._enemies);
            result.Sort((a, b) => b.Health - a.Health);

            return result.ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            List<IEnemy> slowerEnemies = new List<IEnemy>();

            for (int i = 0; i < this._enemies.Count; i++)
            {
                if (this._enemies[i].AttackSpeed < speed)
                {
                    slowerEnemies.Add(this._enemies[i]);
                }
            }

            return slowerEnemies;
        }

        public IEnemy GetSlowest()
        {
            this.EnsureNotEmtpy();

            return this._enemies.GetLast();
        }

        public void ShootFastest()
        {
            this.EnsureNotEmtpy();

            this._enemies.RemoveFirst();
        }

        public void ShootSlowest()
        {
            this.EnsureNotEmtpy();

            this._enemies.RemoveLast();
        }

        private void EnsureNotEmtpy()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
        }
    }
}
