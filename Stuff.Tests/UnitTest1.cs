﻿namespace Stuff.Tests;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging.Abstractions;
using StackExchange.Redis;

public class Tests
{
    private IContainer container;
    private Worker worker;

    [OneTimeSetUp]
    public async Task Setup()
    {
        container = new ContainerBuilder().
            WithImage("redis/redis-stack:latest").
            WithPortBinding(6379, 6379).
            // WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379)). this unfortunately doesn't work
            Build();
        await container.StartAsync();
        await Task.Delay(3000); // this sucks but it works, apparently we have to wait for redis to start
        worker = new Worker(
            NullLogger<Worker>.Instance, ConnectionMultiplexer.Connect("localhost"));
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        worker.Dispose();
        await container.DisposeAsync();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
