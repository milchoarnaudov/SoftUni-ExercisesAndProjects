namespace _01.RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RoyaleArena : IArena
    {
        private readonly Dictionary<int, BattleCard> cards;
        private readonly Dictionary<CardType, Table<BattleCard>> typesSortedByDamage;
        private readonly Dictionary<string, Table<BattleCard>> namesSortedBySwag;
        private readonly Table<BattleCard> sortedBySwag;

        public RoyaleArena()
        {
            this.cards = new Dictionary<int, BattleCard>();
            this.typesSortedByDamage = new Dictionary<CardType, Table<BattleCard>>();
            this.namesSortedBySwag = new Dictionary<string, Table<BattleCard>>();
            this.sortedBySwag = new Table<BattleCard>(new SwagIndex());
        }

        public void Add(BattleCard card)
        {
            this.cards.Add(card.Id, card);
            this.AddToSearchCollection<DamageIndex>(this.typesSortedByDamage, card, c => c.Type);
            this.AddToSearchCollection<SwagIndex>(this.namesSortedBySwag, card, c => c.Name);
            this.sortedBySwag.Add(card);
        }

        private void AddToSearchCollection<T>(IDictionary dictionary, BattleCard card, Func<BattleCard, object> getKey)
            where T : Index<double>, new()
        {
            var key = getKey(card);

            if (dictionary[key] == null)
            {
                dictionary[key] = new Table<BattleCard>(new T());
            }

            (dictionary[key] as Table<BattleCard>).Add(card);
        }

        

        public bool Contains(BattleCard card)
        {
            return cards.ContainsKey(card.Id);
        }

        public int Count => this.cards.Count;

        public void ChangeCardType(int id, CardType type)
        {
            if (!this.cards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            this.RemoveFromSearchCollection(typesSortedByDamage, this.cards[id], c => c.Damage);
            this.cards[id].Type = type;
            this.AddToSearchCollection<DamageIndex>(this.typesSortedByDamage, this.cards[id], c => c.Type);
        }

        private void RemoveFromSearchCollection(IDictionary dictionary, BattleCard card, Func<BattleCard, object> getKey)
        {
            var key = getKey(card);

            if (dictionary[key] != null)
            {
                var items = dictionary[key] as Table<BattleCard>;
                items.Remove(card);

                if (items.Count() == 0)
                {
                    dictionary.Remove(key);
                }
            }
        }

        public BattleCard GetById(int id)
        {
            if (!this.cards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            return this.cards[id];
        }

        public void RemoveById(int id)
        {
            if (!this.cards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            this.RemoveFromSearchCollection(this.typesSortedByDamage, this.cards[id], c => c.Type);
            this.RemoveFromSearchCollection(this.namesSortedBySwag, this.cards[id], c => c.Name);
            this.sortedBySwag.Remove(this.cards[id]);
            this.cards.Remove(id);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            if (!this.typesSortedByDamage.ContainsKey(type))
            {
                throw new InvalidOperationException();
            }

            return this.typesSortedByDamage[type];
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            if (!this.typesSortedByDamage.ContainsKey(type))
            {
                throw new InvalidOperationException();
            }

            return this.typesSortedByDamage[type]
                .GetViewBetween(lo, hi)
                .OrderBy(c => c); ;
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            if (!this.typesSortedByDamage.ContainsKey(type))
            {
                throw new InvalidOperationException();
            }


            return this.typesSortedByDamage[type]
                .Where(x => x.Damage <= damage)
                .OrderBy(x => x); ;
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            if (!this.namesSortedBySwag.ContainsKey(name))
            {
                throw new InvalidOperationException();
            }

            return this.namesSortedBySwag[name];
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            if (!this.namesSortedBySwag.ContainsKey(name))
            {
                throw new InvalidOperationException();
            }

            return this.namesSortedBySwag[name]?.GetViewBetween(lo, hi);
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (n > this.Count)
            {
                throw new InvalidOperationException();
            }

            return this.sortedBySwag.GetFirstN(n, c => c.Id);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            return this.sortedBySwag.GetViewBetween(lo, hi);
        }


        public IEnumerator<BattleCard> GetEnumerator()
        {
            return this.cards.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}