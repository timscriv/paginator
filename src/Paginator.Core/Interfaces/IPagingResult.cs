using System.Collections.Generic;

namespace Paginator.Core.Interfaces
{
    public interface IPagingResult<T, TMetadata> : IReadOnlyList<T>
    {
        TMetadata Metadata { get; }
    }
}
