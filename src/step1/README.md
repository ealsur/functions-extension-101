# Step 1 - Define your attribute

Every Azure Function binding is defined by an Attribute. The Attribute will contain all the parameters and configuration flags that you, as the author, want to expose to your consumers.

For example, the `CosmosDBAttribute`, defines:

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
    /// A string value indicating the app setting to use as the CosmosDB connection string.
    /// </summary>
    [ConnectionString]
    public string ConnectionStringSetting { get; set; }

And these parameters will let the consumer, when declaring the binding, to pass the `DatabaseName`, `ContainerName`, and `ConnectionStringSetting` to the internal handler. In the following steps we'll see how this internal handler receives these parameters and uses them.

## A note on decorators

You will notice in the sample the usage of `[AutoResolve]`, this decorator lets the Azure Functions runtime know that we want consumers to be able to apply [binding patterns](https://docs.microsoft.com/azure/azure-functions/functions-bindings-expressions-patterns). For example, if the consumer passes `%configName%`, then the Runtime will try to resolve the value by looking up a **Function Application Setting** by the name `configName`.

And `[ConnectionString]` adds the possibility of checking for those values also on the Connection Strings section of the settings.

## A note about types

Only **primitive types** should be used in the parameters and configurations definitions, do not attempt to use any complex or custom type as it will cause a Runtime Exception.

## Actions on this step

1. Change and define your own parameters and configurations in [CosmosDBAttribute.cs](./CosmosDBAttribute.cs).
