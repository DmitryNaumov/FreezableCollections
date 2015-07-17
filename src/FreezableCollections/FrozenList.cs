using System.Collections.Generic;

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
            public static readonly IFrozenList<T> Instance = new FreezableList<T>().Freeze();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "There is no rule for classes implementing IList")]
    public interface IFrozenList<out T> : IReadOnlyList<T>
    {
    }
}