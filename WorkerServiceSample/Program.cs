using WorkerServiceSample;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        var serviceConfig = configuration.GetSection("WorkerConfig");

        services.Configure<WorkerConfiguration>(serviceConfig);
        services.AddHostedService<Worker>();

    })
    .UseWindowsService()
    .Build();

await host.RunAsync();
