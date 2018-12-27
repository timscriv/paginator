namespace Paginator.Core.Models
{
    public class OffsetPagingRequest
    {
        public int Offset { get; }
        public int Limit { get; }

        public OffsetPagingRequest(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }
}
