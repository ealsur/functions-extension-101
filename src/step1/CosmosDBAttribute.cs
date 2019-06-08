using System;
using Microsoft.Azure.WebJobs.Description;

namespace CosmosDBBinding.Step1
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class CosmosDBAttribute : Attribute
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CosmosDBAttribute()
        {
        }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="databaseName">The CosmosDB database name.</param>
        /// <param name="containerName">The CosmosDB container name.</param>
        public CosmosDBAttribute(string databaseName, string containerName)
        {
            DatabaseName = databaseName;
            ContainerName = containerName;
        }

        /// <summary>
        /// The name of the database to which the parameter applies.
        /// May include binding parameters.
        /// </summary>
        [AutoResolve]
        public string DatabaseName { get; private set; }

        /// <summary>
        /// The name of the container to which the parameter applies. 
        /// May include binding parameters.
        /// </summary>
        [AutoResolve]
        public string ContainerName { get; private set; }

        /// <summary>
        /// Optional.
        /// Only applies to output bindings.
        /// If true, the database and collection will be automatically created if they do not exist.
        /// </summary>
        public bool CreateIfNotExists { get; set; }

        /// <summary>
        /// A string value indicating the app setting to use as the CosmosDB connection string.
        /// </summary>
        [ConnectionString]
        public string ConnectionStringSetting { get; set; }

        /// <summary>
        /// Optional.
        /// When specified on an output binding and <see cref="CreateIfNotExists"/> is true, defines the partition key 
        /// path for the created collection.
        /// When specified on an input binding, specifies the partition key value for the lookup.
        /// May include binding parameters.
        /// </summary>
        [AutoResolve]
        public string PartitionKey { get; set; }

        /// <summary>
        /// Optional.
        /// When specified on an output binding and <see cref="CreateIfNotExists"/> is true, defines the throughput of the created
        /// collection.
        /// </summary>
        public int ContainerThroughput { get; set; }

        /// <summary>
        /// Optional.
        /// Defines the region where the Function is running on, so the connection can be made to the closest possible Cosmos DB endpoint.
        /// </summary>
        /// <example>
        /// CurrentRegion = "East US"
        /// </example>
        [AutoResolve]
        public string CurrentRegion { get; set; }
    }
}
