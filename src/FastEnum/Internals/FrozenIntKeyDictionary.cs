﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;



namespace FastEnumUtility.Internals
{
    /// <summary>
    /// Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    /// <remarks>
    /// Reference:
    /// https://github.com/neuecc/MessagePack-CSharp/blob/master/src/MessagePack.UnityClient/Assets/Scripts/MessagePack/Internal/ThreadsafeTypeKeyHashTable.cs
    /// 
    /// This class is int specialized <see cref="FrozenDictionary{TKey, TValue}"/>.
    /// </remarks>
    internal sealed class FrozenIntKeyDictionary<TValue> : IReadOnlyDictionary<int, TValue>
    {
        #region Constants
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        #endregion


        #region Properties
        private Entry[] buckets;
        private int size;
        private readonly float loadFactor;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="bucketSize"></param>
        /// <param name="loadFactor"></param>
        private FrozenIntKeyDictionary(int bucketSize, float loadFactor)
        {
            this.buckets = (bucketSize == 0) ? Array.Empty<Entry>() : new Entry[bucketSize];
            this.loadFactor = loadFactor;
        }
        #endregion


        #region Create
        /// <summary>
        /// Creates a <see cref="FrozenIntKeyDictionary{TValue}"/> from an <see cref="IEnumerable{T}"/> according to a specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static FrozenIntKeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, int> keySelector)
            => Create(source, keySelector, PassThrough);


        /// <summary>
        ///  Creates a <see cref="FrozenIntKeyDictionary{TValue}"/> from an <see cref="IEnumerable{T}"/> according to specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenIntKeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, int> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (valueSelector == null) throw new ArgumentNullException(nameof(valueSelector));

            const int initialSize = 4;
            const float loadFactor = 0.75f;
            var size = source.CountIfMaterialized() ?? initialSize;
            var bucketSize = CalculateCapacity(size, loadFactor);
            var result = new FrozenIntKeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (var x in source)
            {
                var key = keySelector(x);
                var value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                    throw new ArgumentException($"Key was already exists. Key:{key}");
            }

            return result;
        }
        #endregion


        #region Add
        /// <summary>
        /// Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="resultingValue"></param>
        /// <returns></returns>
        private bool TryAddInternal(int key, TValue value, out TValue resultingValue)
        {
            var nextCapacity = CalculateCapacity(this.size + 1, this.loadFactor);
            if (this.buckets.Length < nextCapacity)
            {
                //--- rehash
                var nextBucket = new Entry[nextCapacity];
                for (int i = 0; i < this.buckets.Length; i++)
                {
                    var e = this.buckets[i];
                    while (e != null)
                    {
                        var newEntry = new Entry(e.Key, e.Value, e.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        e = e.Next;
                    }
                }

                var success = AddToBuckets(nextBucket, key, null, value, out resultingValue);
                this.buckets = nextBucket;
                if (success)
                    this.size++;

                return success;
            }
            else
            {
                var success = AddToBuckets(this.buckets, key, null, value, out resultingValue);
                if (success)
                    this.size++;

                return success;
            }

            #region Local Functions
            //--- please pass 'key + newEntry' or 'key + value'.
            static bool AddToBuckets(Entry[] buckets, int newKey, Entry newEntry, TValue value, out TValue resultingValue)
            {
                var hash = newEntry?.Hash ?? newKey.GetHashCode();
                var index = hash & (buckets.Length - 1);
                if (buckets[index] == null)
                {
                    if (newEntry == null)
                    {
                        resultingValue = value;
                        buckets[index] = new Entry(newKey, resultingValue, hash);
                    }
                    else
                    {
                        resultingValue = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    var lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            resultingValue = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next == null)
                        {
                            if (newEntry == null)
                            {
                                resultingValue = value;
                                lastEntry.Next = new Entry(newKey, resultingValue, hash);
                            }
                            else
                            {
                                resultingValue = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }
                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }
                return true;
            }
            #endregion
        }


        /// <summary>
        /// Calculates bucket capacity.
        /// </summary>
        /// <param name="collectionSize"></param>
        /// <param name="loadFactor"></param>
        /// <returns></returns>
        private static int CalculateCapacity(int collectionSize, float loadFactor)
        {
            var initialCapacity = (int)(collectionSize / loadFactor);
            var capacity = 1;
            while (capacity < initialCapacity)
                capacity <<= 1;

            if (capacity < 8)
                return 8;

            return capacity;
        }
        #endregion


        #region Get
        /// <summary>
        /// Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(int key, TValue defaultValue = default)
            => this.TryGetValue(key, out var value)
            ? value
            : defaultValue;
        #endregion


        #region IReadOnlyDictionary<TKey, TValue> implementations
        /// <summary>
        /// Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[int key]
            => this.TryGetValue(key, out var value)
            ? value
            : throw new KeyNotFoundException();


        /// <summary>
        /// Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<int> Keys
            => throw new NotImplementedException();


        /// <summary>
        /// Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
            => throw new NotImplementedException();


        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        public int Count
            => this.size;


        /// <summary>
        /// Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        /// true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public bool ContainsKey(int key)
            => this.TryGetValue(key, out _);


        /// <summary>
        /// Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key, if the key is found;
        /// otherwise, the default value for the type of the value parameter.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}"/> interface contains an element that has the specified key; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(int key, out TValue value)
        {
            var hash = key.GetHashCode();
            var index = hash & (this.buckets.Length - 1);
            var next = this.buckets[index];
            while (next != null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }
                next = next.Next;
            }
            value = default;
            return false;
        }


        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator()
            => throw new NotImplementedException();


        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => throw new NotImplementedException();
        #endregion


        #region Inner Classes
        /// <summary>
        /// Represents <see cref="FrozenStringKeyDictionary{TValue}"/> entry.
        /// </summary>
        private class Entry
        {
            public readonly int Key;
            public readonly TValue Value;
            public readonly int Hash;
            public Entry Next;

            public Entry(int key, TValue value, int hash)
            {
                this.Key = key;
                this.Value = value;
                this.Hash = hash;
            }
        }
        #endregion
    }
}
