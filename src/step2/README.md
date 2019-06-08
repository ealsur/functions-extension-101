# Step 2 - Create your connector or service

Every output binding's goal is to send information somewhere, it could be a Web API, a queue, some other custom service that you've built, or your database of choice.

In this step, we define a **Collector Factory**. The factory will be in charge of creating instances of the connector or service that the binding will use to send whatever items the consumer passes to the binding.

## Actions on this step

1. Add any required external NuGet dependencies in the [Step 2 CSPROJ](./CosmosDBBinding.Step2.csproj). For example, if you plan on saving items in a Mongo database, add the Mongo C# Driver as a reference.
2. Define the Collector Factory and the the parameters it requires to initialize the connector or service, these parameters should be something the consumer can define as part of the [Attribute](../step1/README.md). For example, in [CosmosDBBindingCollectorFactory](./CosmosDBBindingCollectorFactory.cs), I'm receiving the Connection String and Region. The return Type should be whatever connector or service instance you will use to save those items, in this example, the Cosmos DB Client, because I want to save items into an Azure Cosmos account.
3. Expose an interface like [ICosmosDBBindingCollectorFactory](./ICosmosDBBindingCollectorFactory.cs) with the method used to create the connector or service instance.
