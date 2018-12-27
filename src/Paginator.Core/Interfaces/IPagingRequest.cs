using System;
using System.Collections.Generic;
using System.Text;

namespace Paginator.Core.Interfaces
{
    public interface IPagingRequest<TRequest>
    {
        TRequest Value { get; }
    }
}
