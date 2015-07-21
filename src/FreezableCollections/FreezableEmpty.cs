namespace FreezableCollections
{
    public static class FrozenList
    {
        public static IFrozenList<T> Empty<T>()
        {
            return EmptyFrozenList<T>.Instance;
        }

        private static class EmptyFrozenList<T>
        {
            public static readonly IFrozenList<T> Instance = new FreezableList<T>(0).Freeze();
        }
    }

    public static class FrozenDictionary
    {
        public static IFrozenDictionary<TKey, TValue> Empty<TKey, TValue>()
        {
            return EmptyFrozenDictionary<TKey, TValue>.Instance;
        }

        private static class EmptyFrozenDictionary<TKey, TValue>
        {
            public static readonly IFrozenDictionary<TKey, TValue> Instance = new FreezableDictionary<TKey, TValue>(0).Freeze();
        }
    }
}