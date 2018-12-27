namespace Paginator.Core.Models
{
    public class CursorPagingRequest
    {
        public string Cursor { get; }
        public int Limit { get; }

        public CursorPagingRequest(string cursor, int limit)
        {
            Cursor = cursor;
            Limit = limit;
        }
    }
}
