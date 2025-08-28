using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ScalpLedger.Application.Exchanges.Adapters.ErrorCodes;

namespace ScalpLedger.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        //NOTE: don't make your Validators internal, this method won't work if you do it. and you need to use custom DI to introduce each Validator!
        // services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(c => { c.RegisterServicesFromAssembly(assembly); });

        services.AddSingleton<ExchangeErrorCodeFactory>();
    }
}