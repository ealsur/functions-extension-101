using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CosmosDBBinding.Step3
{
    public class CosmosDBBindingAsyncCollector<T>: IAsyncCollector<T>
    {
        private CosmosDBBindingContext cosmosContext;

        public CosmosDBBindingAsyncCollector(CosmosDBBindingContext cosmosContext) => this.cosmosContext = cosmosContext;

        public async Task AddAsync(
            T item, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (this.cosmosContext.ResolvedAttribute.CreateIfNotExists) 
            {
                await InitializeContainer(this.cosmosContext);
            }

            await UpsertDocument(this.cosmosContext, item);
        }

        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // no-op
            return Task.FromResult(0);
        }

        private static async Task InitializeContainer(CosmosDBBindingContext context)
        {
            DatabaseResponse databaseResponse = await context.CosmosClient.GetDatabase(context.ResolvedAttribute.DatabaseName).ReadAsync();
            if (databaseResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await context.CosmosClient.CreateDatabaseAsync(context.ResolvedAttribute.DatabaseName);
            }

            ContainerResponse containerResponse = await context.CosmosClient.GetContainer(context.ResolvedAttribute.DatabaseName, context.ResolvedAttribute.ContainerName).ReadAsync();
            if (containerResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await context.CosmosClient.GetDatabase(context.ResolvedAttribute.DatabaseName).CreateContainerAsync(
                    new CosmosContainerSettings(context.ResolvedAttribute.ContainerName, context.ResolvedAttribute.PartitionKey),
                    context.ResolvedAttribute.ContainerThroughput
                );
            }
        }

        private static async Task UpsertDocument(CosmosDBBindingContext context, T item)
        {
            // DocumentClient does not accept strings directly.
            object convertedItem = item;
            if (item is string)
            {
                convertedItem = JObject.Parse(item.ToString());
            }

            await context.CosmosClient
                .GetContainer(context.ResolvedAttribute.DatabaseName,
                context.ResolvedAttribute.ContainerName)
                .UpsertItemAsync<T>(item);
        }
    }
}
