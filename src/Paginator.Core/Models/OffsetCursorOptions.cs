using Paginator.Core.Interfaces;

namespace Paginator.Core.Models
{
    public class OffsetCursorOptions : ICursorOptions
    {
        public int Limit { get; }

        public OffsetCursorOptions(int limit)
        {
            Limit = limit;
        }
    }
}
