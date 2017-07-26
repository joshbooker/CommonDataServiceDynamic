using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CommonDataService;
using Microsoft.CommonDataService.CommonEntitySets;
using Microsoft.CommonDataService.Configuration;
using Microsoft.CommonDataService.ServiceClient.Security;
using Microsoft.CommonDataService.Entities;

using System.Linq.Dynamic;
//using System.Linq.Expressions;

namespace CDS_Console
{

    class Program
    {   [STAThread] 
        static void Main(string[] args)
        {
            using (var client = ConnectionSettings.Instance.CreateClient().Result)
            {
                //var query2 = client.GetRelationalEntitySet("Microsoft.CommonDataService.CommonEntities", "ProductCategory", Microsoft.CommonDataService.Version.Create("1.0.0"))
                var query2 = client.GetRelationalEntitySet<ProductCategory>()
                        .CreateQueryBuilder()
                        //.Where(pc => pc.Name == "Surface" || pc.Name == "Phone")
                        .Where("Name = \"Surface\" OR Name = \"Phone\"")
                        .Project(pc => pc.SelectField(f => f["CategoryId"]).SelectField(f => f["Name"]));

                OperationResult<IReadOnlyList<ProductCategory>> queryResult = null;
                client.CreateRelationalBatchExecuter(RelationalBatchExecutionMode.Transactional)
                     .Query(query2, out queryResult)
                     .ExecuteAsync().Wait();

                
                 foreach (var entry in queryResult.Result)
                 {
                    Console.WriteLine(entry.Name);
                 }
            }
        }
    }
}
