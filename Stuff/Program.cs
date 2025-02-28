using Stuff;

using StackExchange.Redis;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(provider => ConnectionMultiplexer.Connect("localhost"));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
