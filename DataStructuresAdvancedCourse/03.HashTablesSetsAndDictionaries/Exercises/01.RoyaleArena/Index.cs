namespace _01.RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class Index<TKey> : IComparer<BattleCard>, IEnumerable<TKey>
        where TKey : IComparable<TKey>
    {
        public abstract Func<BattleCard, TKey> GetKey { get; }

        protected abstract SortedSet<TKey> Keys { get; }

        public TKey Min => Keys.Min;

        public TKey Max => Keys.Max;

        public int Count => Keys.Count;

        public void Add(TKey key)
        {
            this.Keys.Add(key);
        }

        public void Remove(TKey key)
        {
            this.Keys.Remove(key);
        }

        public IEnumerable<TKey> GetViewBetween(TKey min, TKey max)
        {
            return this.Keys.GetViewBetween(min, max);
        }

        public virtual int Compare(BattleCard x, BattleCard y)
        {
            return this.GetKey(x).CompareTo(this.GetKey(y));
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return this.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
