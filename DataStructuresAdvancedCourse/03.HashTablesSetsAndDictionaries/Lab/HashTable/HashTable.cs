namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int InitialCapacity = 100;
        private List<KeyValue<TKey, TValue>>[] buckets;

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return buckets.Length;
            }
        }

        public HashTable()
            : this(InitialCapacity)
        {
        }

        public HashTable(int capacity)
        {
            buckets = new List<KeyValue<TKey, TValue>>[capacity];
        }

        public void Add(TKey key, TValue value)
        {
            if (this.ContainsKey(key))
            {
                throw new ArgumentException();
            }


            var item = new KeyValue<TKey, TValue>(key, value);
            this.AddItem(item);
            this.Count++;
            this.ResizeAndRefresh();
        }

        private void AddItem(KeyValue<TKey, TValue> item)
        {
            var index = this.GetIndex(item.Key);

            if (buckets[index] == null)
            {
                buckets[index] = new List<KeyValue<TKey, TValue>>();
            }

            buckets[index].Add(item);
        }

        private int GetIndex(TKey key)
        {
            var hash = key.GetHashCode();

            return Math.Abs(hash % Capacity);
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            var item = Find(key);

            if (item != null)
            {
                item.Value = value;
            }
            else
            {
                this.Add(key, value);
            }

            return true;
        }

        public TValue Get(TKey key)
        {
            return this.GetItemOrThrowException(key);
        }

        private TValue GetItemOrThrowException(TKey key)
        {
            var item = Find(key);

            if (item != null)
            {
                return item.Value;
            }

            throw new KeyNotFoundException();
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.GetItemOrThrowException(key);
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            var item = this.Find(key);

            if (item != null)
            {
                value = item.Value;
            }

            return item != null;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            var index = this.GetIndex(key);

            return buckets[index]?.FirstOrDefault(item => item.Key.Equals(key));
        }

        public bool ContainsKey(TKey key)
        {
            return this.Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            var index = this.GetIndex(key);
            var item = this.Find(key);

            if (item != null)
            {
                buckets[index].Remove(item);
                buckets[index] = buckets[index].Count == 0 ? null : buckets[index];
                this.Count--;
                return true;
            }

            return false;
        }

        public void Clear()
        {
            this.buckets = new List<KeyValue<TKey, TValue>>[InitialCapacity];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return buckets.Where(item => item != null).SelectMany(bucket => bucket.Select(x => x.Key));
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return buckets.Where(item => item != null).SelectMany(bucket => bucket.Select(x => x.Value));
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var bucket in buckets)
            {
                if (bucket == null)
                {
                    continue;
                }

                foreach (var item in bucket)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ResizeAndRefresh()
        {
            if (this.Count / (double)Capacity >= 0.75)
            {
                var oldBuckets = buckets;
                buckets = new List<KeyValue<TKey, TValue>>[this.Capacity * 2];

                foreach (var bucket in oldBuckets)
                {
                    if (bucket == null)
                    {
                        continue;
                    }

                    foreach (var item in bucket)
                    {
                        this.AddItem(item);
                    }
                }
            }
        }
    }
}
