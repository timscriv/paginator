using System.Linq;

namespace Paginator.Core.Interfaces
{
    public interface ICursor<T, TMetadata, TOptions> where TOptions : ICursorOptions
    {
        IPagingResult<T, TMetadata> ApplyCursor(IQueryable<T> query, TOptions options);
    }
}
