# Step 3 - Define your Collector

Your output binding's goal is to send items to your connector or service.

In the [previous step](../step2/README.md), you defined the Factory that will create instances of your connector or service.

In this step, we will define the `IAsyncCollector<T>` implementation that will receive these items and process them through an instance of your connector or service.

`IAsyncCollector<T>` defines two methods:

* Task AddAsync(T item,  CancellationToken cancellationToken = default(CancellationToken))
* Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))

You can implement them in your own custom way, depending on how your connector or service can process items and what kind of experience you want the consumer to have.

`AddAsync` will be called each time the consumer wants to pass down an item into your binding, you could choose to **process it at this time** (like in the example), or **add it to a batch**.
`FlushAsync` will be called at the end of the Function execution, if you opted for adding items to a batch, this is the right time to process them all.

## Actions on this step

1. Define a [Context](./CosmosDBBindingContext.cs), it should contain both a reference to your Attribute and a reference to an instance of your connector or service. We will see [how this Context is created in the next step](../step4/README.md).
2. Define your own implementation of `AsyncCollector<T>`, like [CosmosDBBindingAsyncCollector](./CosmosDBBindingAsyncCollector.cs). Your Collector will be receiving a Context on the constructor that can be used in `AddAsync` and/or `FlushAsync`.
