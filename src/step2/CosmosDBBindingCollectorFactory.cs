using System;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace CosmosDBBinding.Step2
{
    public class CosmosDBBindingCollectorFactory : ICosmosDBBindingCollectorFactory
    {
        public CosmosClient CreateClient(
            string connectionString,
            string currentRegion = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(connectionString);
            if (!string.IsNullOrEmpty(currentRegion))
            {
                clientBuilder.WithApplicationRegion(currentRegion);
            }

            return clientBuilder.Build();
        }
    }
}
