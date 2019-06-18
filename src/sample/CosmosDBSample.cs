using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using CosmosDBBinding.Step1;
using System.Threading.Tasks;

namespace CosmosDBBinding.Sample
{
    public static class CosmosDBSample
    {
        [FunctionName("CosmosDBSample")]
        public static async Task Run(
            [TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, 
            [CosmosDB("%CosmosDBDatabase%", "%CosmosDBContainer%", ConnectionStringSetting = "CosmosDBConnectionString")] IAsyncCollector<MyClass> CosmosDBCollector,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Create some random item
            MyClass item = new MyClass()
            {
                id = Guid.NewGuid().ToString(),
                SomeData = "some random data"
            };

            // Send it to the collector
            await CosmosDBCollector.AddAsync(item);
        }
    }

    public class MyClass
    {
        public string id { get; set; }
        public string SomeData { get; set; }
    }
}
