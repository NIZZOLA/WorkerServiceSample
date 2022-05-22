using Serilog;
using WorkerServiceSample;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        var serviceConfig = configuration.GetSection("WorkerConfig");
        var loggerPath = configuration.GetValue<string>("LoggerBasePath");
        var template = configuration.GetValue<string>("LoggerFileTemplate");
        
        services.Configure<WorkerConfiguration>(serviceConfig);
        var shortdate = DateTime.Now.ToString("yyyy-MM-dd_HH");
        var fileName = $"{loggerPath}\\{shortdate}.log";

        Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .WriteTo.Console(outputTemplate: template)
                        .WriteTo.File(fileName, outputTemplate: template)
                        .CreateLogger();

        services.AddHostedService<Worker>();       
    })
    .UseSerilog()
    .UseWindowsService()
    .Build();

await host.RunAsync();