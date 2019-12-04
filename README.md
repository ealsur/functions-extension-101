# How to create your own Azure Function bindings!

Watch the recording! 👇

[![Watch the On Dotnet episode](http://img.youtube.com/vi/vKrUn9qiUI8/0.jpg)](http://www.youtube.com/watch?v=vKrUn9qiUI8 "On Dotnet Episode")

This repository acts as a live learning sample of how you can create your own custom **Azure Functions Output Binding** in **5 easy steps**.

Each step is represented by one project that references the previous one, so any change you make in one, will be picked up by the next one.

> The code sample shows how to create an Output Binding for the [Azure Cosmos DB V3 SDK](https://github.com/Azure/azure-cosmos-dotnet-v3), but it is only for learning purposes, **this is not the official Azure Cosmos DB Output Binding for the V3 SDK**.

The guide is comprised of the following steps:

* [Step 1](./src/step1) - Define your attribute
* [Step 2](./src/step2) - Create your connector or service
* [Step 3](./src/step3) - Define your Collector
* [Step 4](./src/step4) - The glue that binds it together
* [Step 5](./src/step5) - Activating the extension

Each step contains the smallest piece of code possible to understand the base concepts required to build your own Output Binding.

At the end, there is a [sample Azure Functions project](./src/sample) that consumes the result of *Step 5* and can be used for testing.
