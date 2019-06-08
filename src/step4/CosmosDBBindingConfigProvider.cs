using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CosmosDBBinding.Step1;
using CosmosDBBinding.Step2;
using CosmosDBBinding.Step3;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;

namespace CosmosDBBinding.Step4
{
    [Extension("CosmosDBV3")]
    public class CosmosDBBindingConfigProvider : IExtensionConfigProvider
    {
        private readonly ICosmosDBBindingCollectorFactory cosmosBindingCollectorFactory;

        private ConcurrentDictionary<string, CosmosClient> CollectorCache { get; } = new ConcurrentDictionary<string, CosmosClient>();

        public CosmosDBBindingConfigProvider(ICosmosDBBindingCollectorFactory cosmosBindingCollectorFactory)
        {
            this.cosmosBindingCollectorFactory = cosmosBindingCollectorFactory;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var rule = context.AddBindingRule<CosmosDBAttribute>();
            rule.AddValidator(ValidateConnection);
            rule.BindToCollector<CosmosDBBindingOpenType>(typeof(CosmosDBBindingConverter<>), this);
        }

        internal CosmosDBBindingContext CreateContext(CosmosDBAttribute attribute)
        {
            CosmosClient client = GetService(attribute.ConnectionStringSetting, attribute.CurrentRegion);

            return new CosmosDBBindingContext
            {
                CosmosClient = client,
                ResolvedAttribute = attribute,
            };
        }

        private CosmosClient GetService(string connectionString, string region)
        {
            string cacheKey = BuildCacheKey(connectionString, region);
            return CollectorCache.GetOrAdd(cacheKey, (c) => this.cosmosBindingCollectorFactory.CreateClient(connectionString, region));
        }

        internal void ValidateConnection(CosmosDBAttribute attribute, Type paramType)
        {
            if (string.IsNullOrEmpty(attribute.ConnectionStringSetting))
            {
                string attributeProperty = $"{nameof(CosmosDBAttribute)}.{nameof(CosmosDBAttribute.ConnectionStringSetting)}";
                throw new InvalidOperationException(
                    $"The CosmosDB connection string must be set via the {attributeProperty} property.");
            }
        }

        private static string BuildCacheKey(string connectionString, string region) => $"{connectionString}|{region}";

        private class CosmosDBBindingOpenType : OpenType.Poco
        {
            public override bool IsMatch(Type type, OpenTypeMatchContext context)
            {
                if (type.IsGenericType
                    && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return false;
                }

                if (type.FullName == "System.Object")
                {
                    return true;
                }

                return base.IsMatch(type, context);
            }
        }
    }
}
