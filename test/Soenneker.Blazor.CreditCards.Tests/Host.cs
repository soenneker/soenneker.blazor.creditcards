using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Soenneker.Blazor.CreditCards.Registrars;
using Soenneker.Blazor.MockJsRuntime.Registrars;
using Soenneker.TestHosts.Unit;
using Soenneker.Utils.Test;

namespace Soenneker.Blazor.CreditCards.Tests;

public sealed class Host : UnitTestHost
{
    public override Task InitializeAsync()
    {
        SetupIoC(Services);

        Services.AddMockJsRuntimeAsScoped();

        return base.InitializeAsync();
    }

    private static void SetupIoC(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddSerilog(dispose: false);
        });

        IConfiguration config = TestUtil.BuildConfig();
        services.AddSingleton(config);

        services.AddCreditCardsInteropAsScoped();
    }
}
