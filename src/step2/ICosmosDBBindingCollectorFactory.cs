using System;
using Microsoft.Azure.Cosmos;

namespace CosmosDBBinding.Step2
{
    public interface ICosmosDBBindingCollectorFactory
    {
        CosmosClient CreateClient(
            string connectionString,
            string currentRegion = null);
    }
}
