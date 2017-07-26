# CommonDataServiceDynamic

this is a console app which demonstrates extending the [CDS API](https://docs.microsoft.com/en-us/common-data-service/entity-reference/cds-sdk-manipulate-data) to enable dynamic queries.  It extends the `.Where` method to accept strings as criteria and uses the [`DynamicExpression`](https://github.com/kahanu/System.Linq.Dynamic/wiki/Dynamic-Expressions#dynamic-expression-api) class of the [Linq Dynamic Query Library](https://github.com/kahanu/System.Linq.Dynamic/blob/master/Src/System.Linq.Dynamic/DynamicLinq.cs) to parse strings into valid lambda expressions.

## Extension method:

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

```C#
var query = client.GetRelationalEntitySet<ProductCategory>()
        .CreateQueryBuilder()
        //instead of:
        //.Where(pc => pc.Name == "Surface" || pc.Name == "Phone")
        //this:
        .Where("Name = \"Surface\" OR Name = \"Phone\"")
        .Project(pc => pc.SelectField(f => f["CategoryId"]).SelectField(f => f["Name"]));
```

## More Info:

[Expression Language](https://github.com/kahanu/System.Linq.Dynamic/wiki/Dynamic-Expressions#expression-language)

