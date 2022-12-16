using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ConsoleApp.Operation;
using ConsoleDI.Example.Extensions;

try
{
    // Crio um builder de host genérico
    IHostBuilder host = Host.CreateDefaultBuilder(args);

    // Configuro os serviços do host no builder
    host.ConfigureServices((_, services) =>
        services.AddDependencyInjection()
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
    Console.WriteLine("..");
}