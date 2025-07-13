using Grpc.Core;
using Infrastructure;
using Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Create the service container

// ... instantiate
var serviceCollection = new ServiceCollection();

// ... add logging
serviceCollection.AddLogging(config =>
{
    config.AddConsole();
    config.SetMinimumLevel(LogLevel.Information);
});

// ... register services
serviceCollection.AddSingleton<PingService>();
serviceCollection.AddSingleton<ArrowService>();

// Build the service provider

// ... instantiate
var serviceProvider = serviceCollection.BuildServiceProvider();

// ... resolve the service instances
var pingService = serviceProvider.GetRequiredService<PingService>();
var arrowService = serviceProvider.GetRequiredService<ArrowService>();

// ... build the ServerServiceDefinition
var serviceDefinition = ServerServiceDefinition.CreateBuilder()
    .AddMethod(PingDefinition.SayPingMethod, pingService.SayPing)
    .AddMethod(ArrowDefinition.SayArrowMethod, arrowService.SayArrow)
    .Build();

// Create and start the gRPC server

// ... instantiate the server
Server server = new Server
{
    Services = { serviceDefinition },
    Ports = { new ServerPort("localhost", 5000, ServerCredentials.Insecure) }
};

// ... start the server
server.Start();

// ... display output
Console.WriteLine("C# gRPC server is running at localhost:5000");

// ... keep running
await Task.Delay(Timeout.Infinite);

// Production code requires graceful shutdown to allow gRPC to release resources etc
