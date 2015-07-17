using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace FreezableCollections
{
    public class FreezableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> _dictionary;

        public FreezableDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public FreezableDictionary(int capacity, IEqualityComparer<TKey> comparer = null)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        public bool IsFrozen { get; private set; }

        public void Freeze()
        {
            Interlocked.Exchange(ref _dictionary, new ReadOnlyDictionary<TKey, TValue>(_dictionary));

            IsFrozen = true;
        }

        #region Delegated members

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public IEnumerable<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerable<TValue> Values
        {
            get { return _dictionary.Values; }
        }

        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_dictionary).CopyTo(array, index);
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)_dictionary).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)_dictionary).SyncRoot; }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return IsFrozen; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Remove(item);
        }

        void IDictionary.Add(object key, object value)
        {
            ((IDictionary)_dictionary).Add(key, value);
        }

        bool IDictionary.Contains(object key)
        {
            return ((IDictionary)_dictionary).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary) _dictionary).GetEnumerator();
        }

        bool IDictionary.IsFixedSize
        {
            get { return ((IDictionary)_dictionary).IsFixedSize; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return IsFrozen; }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary)_dictionary).Keys; }
        }

        void IDictionary.Remove(object key)
        {
            ((IDictionary) _dictionary).Remove(key);
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary)_dictionary).Values; }
        }

        object IDictionary.this[object key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return _dictionary.Keys; }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return _dictionary.Values; }
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion
    }
}