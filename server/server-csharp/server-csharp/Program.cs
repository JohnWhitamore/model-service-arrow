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

// Register your PingService
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

Console.WriteLine("✅ C# gRPC server is running at localhost:5000");
Console.WriteLine("Press any key to stop...");

// Wait for user input before shutting down
Console.ReadKey();

server.ShutdownAsync().Wait();
Console.WriteLine("🛑 Server stopped");




// original

//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Server.Kestrel.Core;
//using Services;
//using Infrastructure;
//using Microsoft.AspNetCore.Hosting;
//using Grpc.Core;

//var builder = WebApplication.CreateBuilder(args);

//// Configure Kestrel for HTTP/2, required for gRPC
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenLocalhost(5000, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http2;
//    });
//});

//// Register services
//builder.Services.AddGrpc();
//builder.Services.AddSingleton<PingService>();

//var app = builder.Build();

//app.UseRouting();

// attempt 1

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGrpcMethod<PingService>(
//        PingDefinition.SayPingMethod,
//        (service, request, context) => service.SayPing(request, context));
//});

// attempt 2

//var pingService = app.Services.GetRequiredService<PingService>();

//var serviceDefinition = ServerServiceDefinition.CreateBuilder()
//    .AddMethod(PingDefinition.SayPingMethod, pingService.SayPing)
//    .Build();

//app.MapGrpcService(() => serviceDefinition);

//app.Run();