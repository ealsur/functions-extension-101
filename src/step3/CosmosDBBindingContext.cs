using CosmosDBBinding.Step1;
using Microsoft.Azure.Cosmos;

namespace CosmosDBBinding.Step3
{
    public class CosmosDBBindingContext
    {
        public CosmosDBAttribute ResolvedAttribute { get; set; }

        public CosmosClient CosmosClient { get; set; }
    }
}
