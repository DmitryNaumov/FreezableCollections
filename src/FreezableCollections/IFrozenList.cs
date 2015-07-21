using System.Collections.Generic;

namespace FreezableCollections
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "There is no rule for classes implementing IList")]
    public interface IFrozenList<out T> : IReadOnlyList<T>
    {
    }
}