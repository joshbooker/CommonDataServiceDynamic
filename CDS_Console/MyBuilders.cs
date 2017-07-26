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
        /*public static RelationalEntitySet GetRelationalEntitySet(Client client, string @namespace, string name, Version version)
        {
            return client.GetRelationalEntitySet(@namespace, name, version);// Microsoft.CommonDataService.Entities.TypedRelationalEntitySet;
        }
        public static RelationalEntitySet<TEntity>  GetRelationalEntitySet<TEntity>(Client client) where TEntity : TypedRelationalEntity, new()
        {
            return client.GetRelationalEntitySet<TEntity>();// Microsoft.CommonDataService.Entities.TypedRelationalEntitySet;
        }
        public static FromClauseBuilder<TEntity>  MyCreateQueryBuilder<TEntity>(this RelationalEntitySet<TEntity> entitySet) where TEntity : RelationalEntity//, new()
        {
            return entitySet.CreateQueryBuilder<TEntity>();
        }*/
        public static Builders.WhereClauseBuilder<TEntity> Where<TEntity>(this Builders.FromClauseBuilder<TEntity> entitySet, string predicate) where TEntity : RelationalEntity//, new()
        {
            return entitySet.Where(WhereExpression<TEntity>(predicate));
        }
        public static Expression<Func<T,bool>> WhereExpression<T>(string predicate, params object[] values)
        {
            return System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(predicate, values);
        }
    }
}
