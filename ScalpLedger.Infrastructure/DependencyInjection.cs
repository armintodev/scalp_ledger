using Microsoft.Extensions.DependencyInjection;
using ScalpLedger.Application.Common.Databases;
using ScalpLedger.Application.Exchanges.Adapters;
using ScalpLedger.Domain.Repositories;
using ScalpLedger.Infrastructure.Clients.Sockets.ClientPerUser;
using ScalpLedger.Infrastructure.Exchanges.Bitunix;
using ScalpLedger.Infrastructure.Persistence;
using ScalpLedger.Infrastructure.Persistence.Interceptors;
using ScalpLedger.Infrastructure.Persistence.Repositories;
using ScalpLedger.Infrastructure.Persistence.UnitOfWork;

namespace ScalpLedger.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<UpdateAuditableInterceptor>();

        services.AddDbContext<LedgerDbContext>(
            (sp, a) =>
            {
                a.UseNpgsql(connectionString);

                a.AddInterceptors(sp.GetRequiredService<UpdateAuditableInterceptor>());
            }
        );

        services.AddScoped<IUnitOfWork, UnitOfWork<LedgerDbContext>>();

        services.AddScoped<ICandleRepository, CandleRepository>();

        services.AddSingleton<IPerUserHttpClientFactory, PerUserHttpClientFactory>();

        services.AddScoped<IExchangeAdapter, BitunixAdapter>();
    }
}