using Paginator.Core.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace Paginator.Core.Models
{
    public class PagingResult<T, TMetadata> : IPagingResult<T, TMetadata>
    {
        private IReadOnlyList<T> Data { get; }
        public TMetadata Metadata { get; }

        public T this[int index] => Data[index];
        public int Count => Data.Count;

        public PagingResult(IReadOnlyList<T> data, TMetadata metadata)
        {
            Data = data;
            Metadata = metadata;
        }

        public IEnumerator<T> GetEnumerator() => Data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
