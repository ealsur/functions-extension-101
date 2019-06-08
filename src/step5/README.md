# Step 5 - Activating the extension

This step is rather simple, in it, we will initialize the `IExtensionConfigProvider` implementation we did in [Step 4](../step4/README.md) and add it to the consumer's Function App project whenever they reference our Extension.

The [`IWebJobsBuilder` static method](./CosmosDBWebJobsBuilderExtensions.cs) simply registers your extension by calling `AddExtension` and passing your `IExtensionConfigProvider` implementation, and it also defines a Singleton instance of your Collector Factory, so whenever your `IExtensionConfigProvider` is created, it will receive this Factory to create the connector or services instances. 

The [`IWebJobsStartup` implementation](./CosmosDBWebJobsStartup.cs) is what will automatically initialize it as part of the Functions Runtime initialization.

## Actions in this step

1. Create a static extension method that will:
    1. Register your `IExtensionConfigProvider` implementation with `AddExtension`.
    2. Register your Collector Factory with `AddSingleton`.
2. Create an `IWebJobsStartup` implementation that will call your extension method as part of `Configure(IWebJobsBuilder builder)`.
