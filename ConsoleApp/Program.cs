using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ConsoleApp.Operation.Interfaces;
using ConsoleApp.Operation;

try
{
    // Crio um builder de host genéricojh
    IHostBuilder host = Host.CreateDefaultBuilder(args);

    // Configuro os serviços do host
    host.ConfigureServices((_, services) =>
        services
            .AddTransient<ITransientOperation, DefaultOperation>()
            .AddScoped<IScopedOperation, DefaultOperation>()
            .AddSingleton<ISingletonOperation, DefaultOperation>()
            .AddTransient<OperationLogger>()
    );
    using IHost app = host.Build();
    ExemplifyingScoping(app.Services, "Scope 1");
    ExemplifyingScoping(app.Services, "Scope 2");

    await app.RunAsync();
}
catch (Exception e)
{
    throw new Exception($"Erro ao iniciar a aplicação! {e.Message}");
}


static void ExemplifyingScoping(IServiceProvider services, string scope)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    OperationLogger logger = provider.GetRequiredService<OperationLogger>();
    logger.LogOperations($"{scope}-Call 1 .GetRequiredService<OperationLogger>");
    Console.WriteLine("..");

    logger = provider.GetRequiredService<OperationLogger>();
    logger.LogOperations($"{scope}-Call 2 .GetRequiredService<OperationLogger>");
}