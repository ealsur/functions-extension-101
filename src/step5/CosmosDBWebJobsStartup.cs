using CosmosDBBinding.Step5;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Hosting;

[assembly: WebJobsStartup(typeof(CosmosDBWebJobsStartup))]

namespace CosmosDBBinding.Step5
{
    public class CosmosDBWebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddCosmosDBV3();
        }
    }
}