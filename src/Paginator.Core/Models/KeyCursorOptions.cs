using Paginator.Core.Interfaces;
using System;
using System.Linq.Expressions;

namespace Paginator.Core.Models
{
    public class KeyCursorOptions<TEntity, TKey> : ICursorOptions
    {
        public int Limit { get; } = 100;

        public Func<TEntity, TKey> KeySelector { get; }

        public Expression<Func<TEntity, bool>> KeyEvaluator { get; }

        public KeyCursorOptions(
            Expression<Func<TEntity, bool>> keyEvaluator,
            Func<TEntity, TKey> keySelector,
            int limit = 100)
        {
            if (keyEvaluator == null || keySelector == null) throw new ArgumentException("KeyEvaluator and KeySelector must be set.");

            Limit = limit;
            KeySelector = keySelector;
            KeyEvaluator = keyEvaluator;
        }
    }
}
