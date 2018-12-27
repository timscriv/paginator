namespace Paginator.Core.Models
{
    public class OffsetPagingMetadata
    {
        public int Count { get; }
        public int Total { get; }
        public int Limit { get; }
        public int Offset { get; }

        public OffsetPagingMetadata(int count, int total, int limit, int offset)
        {
            Count = count;
            Total = total;
            Limit = limit;
            Offset = offset;
        }
    }
}
