using System;
using CosmosDBBinding.Step2;
using CosmosDBBinding.Step4;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosDBBinding.Step5
{
    public static class CosmosDBWebJobsBuilderExtensions
    {
        public static IWebJobsBuilder AddCosmosDBV3(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            builder.AddExtension<CosmosDBBindingConfigProvider>();

            builder.Services.AddSingleton<ICosmosDBBindingCollectorFactory, CosmosDBBindingCollectorFactory>();

            return builder;
        }
    }
}
