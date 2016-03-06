using System.Collections.Generic;
using System;

namespace Singular.Evolution.Utils
{
    /// <summary>
    /// Implements a concurrent LRU cache with a given capacity
    /// Is important to note that the cache does a lock on Read and Write. Thus, it is not recommended for heavy concurrent use. Also,
    /// the cache is not optimized for perfomance.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SimpleLRUCache<K, V>
    {
        private readonly Dictionary<K, KeyValuePair<LinkedListNode<K>, V>> cache = new Dictionary<K, KeyValuePair<LinkedListNode<K>, V>>();
        private readonly LinkedList<K> fetchOrder = new LinkedList<K>();

        private readonly object lockObj = new object();

        /// <summary>
        /// Gets or sets the size of the cache.
        /// </summary>
        /// <value>
        /// The size of the cache.
        /// </value>
        public long CacheMaximumSize { get; set; } = 65536;

        /// <summary>
        /// Gets or sets the ratio between the size of the cache size after the cleanup and the <see cref="CacheMaximumSize"/>.
        /// </summary>
        /// <value>
        /// The size of the cache cleanup ratio.
        /// </value>
        public float CacheCleanupRatio { get; set; } = 0.7f;


        /// <summary>
        /// Gets the number of elements in the cache.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public long Count
        {
            get
            {
                lock (lockObj)
                {
                    return cache.Count;
                }   
            }
        }

        /// <summary>
        /// Adds the specified key value pair to the cache.
        /// If the added key already exists, the existent value is replaced.
        /// It must be noted that if a new element is added, and the <see cref="CacheMaximumSize"/> is reached a
        /// CleanUp operation would be executed, thus, blocking the method return until it is finalized
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(K key, V value)
        {
            lock (lockObj)
            {
                KeyValuePair<LinkedListNode<K>,V> item;
                if (cache.TryGetValue(key, out item))
                {
                    fetchOrder.Remove(item.Key);
                }
                fetchOrder.AddFirst(key);
                if (cache.Count >= CacheMaximumSize)
                {
                    Clean();
                }
            }
        }

        private void Clean()
        {
            int targetSize = (int) (CacheMaximumSize*CacheCleanupRatio);
            for (int i = targetSize - 1; i < CacheMaximumSize; i++)
            {
                LinkedListNode<K> lastKey = fetchOrder.Last;
                fetchOrder.RemoveLast();
                cache.Remove(lastKey.Value);
            }
        }

        /// <summary>
        /// Tries to get the value associated with the key in the cached
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The associated value.</param>
        /// <returns>True if the key was found, false otherwise</returns>
        public bool TryGet(K key, out V value)
        {
            lock (lockObj)
            {
                KeyValuePair<LinkedListNode<K>,V> item;
                bool success = cache.TryGetValue(key, out item);
                if (success)
                {
                    fetchOrder.Remove(item.Key);
                    fetchOrder.AddFirst(item.Key);
                }
                value = item.Key == null ? item.Value : default(V);
                return success;
            }
        }
    }
}