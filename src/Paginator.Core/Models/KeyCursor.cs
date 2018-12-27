using Paginator.Core.Exceptions;
using Paginator.Core.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace Paginator.Core.Models
{
    public class KeyCursor<TEntity, TKey> : ICursor<TEntity, CursorPagingMetadata, KeyCursorOptions<TEntity, TKey>>
    {
        private const string CursorType = "key";

        public TKey Key { get; set; }

        public KeyCursor(string encodedCursor)
        {
            if (string.IsNullOrEmpty(encodedCursor)) return;

            var decodedBytes = Convert.FromBase64String(encodedCursor);
            var cursorString = Encoding.UTF8.GetString(decodedBytes);

            if (!cursorString.Contains($"{CursorType}:")) return;

            var cursorParts = cursorString.Split(':');
            try
            {
                Key = (TKey)Convert.ChangeType(cursorParts.Last(), typeof(TKey));
            }
            catch (Exception)
            {
                throw new InvalidCursorException();
            }
        }

        public KeyCursor(TKey key)
        {
            Key = key;
        }

        public IPagingResult<TEntity, CursorPagingMetadata> ApplyCursor(
            IQueryable<TEntity> query,
             KeyCursorOptions<TEntity, TKey> options)
        {
            var data = query
                .Where(options.KeyEvaluator)
                .Take(options.Limit)
                .ToList()
                .AsReadOnly();

            Key = options.KeySelector(data.LastOrDefault());
            //TODO: implement totalCount
            var metadata = new CursorPagingMetadata(data.Count, 0, options.Limit, this.Stringify());

            return new PagingResult<TEntity, CursorPagingMetadata>(data, metadata);
        }

        public string Stringify()
        {
            if (Key == null) return "";

            var bytes = Encoding.UTF8.GetBytes($"{CursorType}:{Key}");
            return Convert.ToBase64String(bytes);
        }
    }
}
