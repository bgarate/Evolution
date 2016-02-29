using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    public class SimpleLRUCache<K, V>
    {
        private readonly Dictionary<K, LinkedListNode<V>> cache = new Dictionary<K, LinkedListNode<V>>();
        private readonly LinkedList<V> fetchOrder = new LinkedList<V>();

        private readonly object lockObj = new object();

        public long CacheSize { get; set; } = 65536;

        public void Add(K key, V value)
        {
            lock (lockObj)
            {
                LinkedListNode<V> item;
                if (!cache.TryGetValue(key, out item))
                {
                    item = new LinkedListNode<V>(value);
                }
                else
                {
                    fetchOrder.Remove(item);
                }
                fetchOrder.AddFirst(item);
                if (cache.Count > CacheSize)
                {
                }
            }
        }

        public bool TryGet(K key, out V value)
        {
            lock (lockObj)
            {
                LinkedListNode<V> item;
                bool success = cache.TryGetValue(key, out item);
                if (success)
                {
                    fetchOrder.Remove(item);
                    fetchOrder.AddFirst(item);
                }
                value = item != null ? item.Value : default(V);
                return success;
            }
        }
    }
}