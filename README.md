# CommonDataServiceDynamic

this is a console app which demonstrates extending the CDS API to enable dynamic queries.  It extends the `.Where` method to accept strings as criteria and uses the `DynamicExpression` class of the [Linq Dynamic Query Library](https://github.com/kahanu/System.Linq.Dynamic/blob/master/Src/System.Linq.Dynamic/DynamicLinq.cs) to parse strings into valid lambda expressions.

## The Extension method is the following:

```C#
using System;

using Microsoft.CommonDataService.Builders;
using Microsoft.CommonDataService.Entities;

namespace Microsoft.CommonDataService
{
    public static class MyBuilders
    {
        public static WhereClauseBuilder<TEntity> Where<TEntity>(this FromClauseBuilder<TEntity> entitySet, string predicate) where TEntity : RelationalEntity//, new()
        {
            return entitySet.Where(WhereExpression<TEntity>(predicate));
        }
        public static System.Linq.Expressions.Expression<Func<T,bool>> WhereExpression<T>(string predicate, params object[] values)
        {
            return System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(predicate, values);
        }
    }
}
```
## Usage:

```
                var query2 = client.GetRelationalEntitySet<ProductCategory>()
                        .CreateQueryBuilder()
                        //.Where(pc => pc.Name == "Surface" || pc.Name == "Phone")
                        .Where("Name = \"Surface\" OR Name = \"Phone\"")
                        .Project(pc => pc.SelectField(f => f["CategoryId"]).SelectField(f => f["Name"]));
```
