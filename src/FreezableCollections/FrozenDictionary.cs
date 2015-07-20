using System.Collections.Generic;

namespace FreezableCollections
{
    public interface IFrozenDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
    }
}