namespace Paginator.Core.Models
{
    public class CursorPagingMetadata
    {
        public int Count { get; }
        public int Total { get; }
        public int Limit { get; }
        public string NextCursor { get; }

        public CursorPagingMetadata(int count, int total, int limit, string nextCursor = null)
        {
            Count = count;
            Total = total;
            Limit = limit;
            NextCursor = nextCursor;
        }
    }
}
