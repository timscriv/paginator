using Paginator.Core.Interfaces;

namespace Paginator.Core.Models
{
    public class PagingRequest<TRequest> : IPagingRequest<TRequest>
    {
        public TRequest Value { get; }

        public PagingRequest(TRequest value)
        {
            Value = value;
        }
    }
}
