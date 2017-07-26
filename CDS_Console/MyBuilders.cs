using System;

using Microsoft.CommonDataService;
using Microsoft.CommonDataService.Builders;
using Microsoft.CommonDataService.Entities;

using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Microsoft.CommonDataService
{
    public static class MyBuilders
    {
        public static WhereClauseBuilder<TEntity> Where<TEntity>(this FromClauseBuilder<TEntity> entitySet, string predicate) where TEntity : RelationalEntity//, new()
        {
            return entitySet.Where(WhereExpression<TEntity>(predicate));
        }
        public static Expression<Func<T,bool>> WhereExpression<T>(string predicate, params object[] values)
        {
            return System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(predicate, values);
        }
    }
}
