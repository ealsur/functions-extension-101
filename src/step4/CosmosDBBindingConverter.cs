using CosmosDBBinding.Step1;
using CosmosDBBinding.Step3;
using Microsoft.Azure.WebJobs;

namespace CosmosDBBinding.Step4
{
    internal class CosmosDBBindingConverter<T> : IConverter<CosmosDBAttribute, IAsyncCollector<T>>
    {
        private readonly CosmosDBBindingConfigProvider configProvider;

        public CosmosDBBindingConverter(CosmosDBBindingConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public IAsyncCollector<T> Convert(CosmosDBAttribute attribute)
        {
            CosmosDBBindingContext context = this.configProvider.CreateContext(attribute);
            return new CosmosDBBindingAsyncCollector<T>(context);
        }
    }
}