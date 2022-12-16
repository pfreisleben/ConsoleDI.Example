using ConsoleApp.Operation;
using ConsoleApp.Operation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDI.Example.Extensions;

public static class AddAppDependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<ITransientOperation, DefaultOperation>()
                .AddScoped<IScopedOperation, DefaultOperation>()
                .AddSingleton<ISingletonOperation, DefaultOperation>()
                .AddTransient<OperationLogger>();
        return services;
    }
}