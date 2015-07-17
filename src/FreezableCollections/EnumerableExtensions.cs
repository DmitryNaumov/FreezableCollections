using System;
using System.Collections.Generic;

namespace FreezableCollections
{
    public static class EnumerableExtensions
    {
        public static FreezableList<T> ToFreezableList<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return new FreezableList<T>(source);
        }

        public static IFrozenList<T> ToFrozenList<T>(this IEnumerable<T> source)
        {
            return ToFreezableList(source).Freeze();
        }
    }
}