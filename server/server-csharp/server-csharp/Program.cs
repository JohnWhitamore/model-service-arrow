using Grpc.Core;
using Infrastructure;
using Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Create the service container
var serviceCollection = new ServiceCollection();

// Add logging
serviceCollection.AddLogging(config =>
{
    config.AddConsole();
    config.SetMinimumLevel(LogLevel.Information);
});

// Register the PingService
serviceCollection.AddSingleton<PingService>();

// Build the provider
var serviceProvider = serviceCollection.BuildServiceProvider();

// Resolve the service instance
var pingService = serviceProvider.GetRequiredService<PingService>();

// Build the ServerServiceDefinition using your manual method descriptor
var serviceDefinition = ServerServiceDefinition.CreateBuilder()
    .AddMethod(PingDefinition.SayPingMethod, pingService.SayPing)
    .Build();

// Create the gRPC server
Server server = new Server
{
    Services = { serviceDefinition },
    Ports = { new ServerPort("localhost", 5000, ServerCredentials.Insecure) }
};

// Start the server
server.Start();

// ... display output
Console.WriteLine("C# gRPC server is running at localhost:5000");
Console.WriteLine("Press any key to stop...");

// Wait for user input before shutting down
Console.ReadKey();

server.ShutdownAsync().Wait();

// ... display output
Console.WriteLine("Server stopped");