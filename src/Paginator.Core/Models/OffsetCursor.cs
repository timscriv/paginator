using Paginator.Core.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace Paginator.Core.Models
{
    public class OffsetCursor<TEntity> : ICursor<TEntity, CursorPagingMetadata, OffsetCursorOptions>
    {
        private const string CursorType = "offset";
        public int Offset { get; set; }

        public OffsetCursor(string encodedCursor)
        {
            if (string.IsNullOrEmpty(encodedCursor)) return;

            var decodedBytes = Convert.FromBase64String(encodedCursor);
            var cursorString = Encoding.UTF8.GetString(decodedBytes);

            if (!cursorString.Contains($"{CursorType}:")) return;

            var cursorParts = cursorString.Split(':');

            if (!int.TryParse(cursorParts.Last(), out int offset)) return;

            Offset = offset;
        }

        public OffsetCursor(int offset)
        {
            Offset = offset;
        }

        public IPagingResult<TEntity, CursorPagingMetadata> ApplyCursor(IQueryable<TEntity> query, OffsetCursorOptions options)
        {
            var data = query
                .Skip(Offset)
                .Take(options.Limit)
                .ToList()
                .AsReadOnly();

            var nextCursor = new OffsetCursor<TEntity>(Offset + data.Count);

            //TODO: implement totalCount
            //TODO: better last item logic
            var nextCursorString = data.Count > 0 ? nextCursor.Stringify() : string.Empty;
            var metadata = new CursorPagingMetadata(data.Count, 0, options.Limit, nextCursorString);

            return new PagingResult<TEntity, CursorPagingMetadata>(data, metadata);
        }

        public string Stringify()
        {
            var bytes = Encoding.UTF8.GetBytes($"{CursorType}:{Offset}");
            return Convert.ToBase64String(bytes);
        }
    }
}
