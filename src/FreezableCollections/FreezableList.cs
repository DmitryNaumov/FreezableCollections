using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace FreezableCollections
{
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "There is no rule for classes implementing IList")]
    public class FreezableList<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        private IList<T> _list;

        public FreezableList()
        {
            _list = new List<T>();
        }

        public FreezableList(int capacity)
        {
            _list = new List<T>(capacity);
        }

        public FreezableList(IEnumerable<T> collection)
        {
            _list = new List<T>(collection);
        }

        public bool IsFrozen { get; private set; }

        public IFrozenList<T> Freeze()
        {
            IsFrozen = true;

            Interlocked.Exchange(ref _list, new ReadOnlyCollection<T>(_list));

            return new FrozenList(this);
        }

        #region Delegated members

        public void Add(T item)
        {
            _list.Add(item);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set
            {
                _list[index] = value;
            }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((IList)_list).IsSynchronized; }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_list).CopyTo(array, index);
        }

        object ICollection.SyncRoot
        {
            get { return ((IList) _list).SyncRoot; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return IsFrozen; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }

        int IList.Add(object value)
        {
            return ((IList)_list).Add(value);
        }

        bool IList.Contains(object value)
        {
            return ((IList)_list).Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return ((IList)_list).IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            ((IList)_list).Insert(index, value);
        }

        bool IList.IsFixedSize
        {
            get { return ((IList)_list).IsFixedSize; }
        }

        bool IList.IsReadOnly
        {
            get { return IsFrozen; }
        }

        void IList.Remove(object value)
        {
            ((IList)_list).Remove(value);
        }

        object IList.this[int index]
        {
            get { return ((IList)_list)[index]; }
            set { ((IList)_list)[index] = value; }
        }

        #endregion

        // NOTE: Inherit from ICollection<T> to support existing LINQ optimizations (like Count())
        private class FrozenList : IFrozenList<T>, ICollection<T>
        {
            private readonly FreezableList<T> _list;

            public FrozenList(FreezableList<T> list)
            {
                if (!list.IsFrozen)
                    throw new InvalidOperationException();

                _list = list;
            }

            #region IReadOnlyList<T>, ICollection<T> members

            public bool Contains(T item)
            {
                return _list.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public T this[int index]
            {
                get { return _list[index]; }
            }

            void ICollection<T>.Add(T item)
            {
                throw new InvalidOperationException();
            }

            void ICollection<T>.Clear()
            {
                throw new InvalidOperationException();
            }

            bool ICollection<T>.IsReadOnly
            {
                get { return true; }
            }

            bool ICollection<T>.Remove(T item)
            {
                throw new InvalidOperationException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)_list).GetEnumerator();
            }

            #endregion
        }
    }
}