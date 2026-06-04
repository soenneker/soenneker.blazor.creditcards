using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.CreditCards.Registrars;

/// <summary>
/// A lightweight Blazor library for realistic, customizable credit and debit card displays with BIN-based styling, issuer detection, and full support for branding and card metadata visualization.
/// </summary>
public static class PaymentCardRegistrar
{
    /// <summary>
    /// Adds <see cref="IPaymentCardInterop"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddPaymentCardAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped();
        services.TryAddScoped<IPaymentCardDisplayService, PaymentCardDisplayService>();
        services.TryAddScoped<IPaymentCardInterop, PaymentCardInterop>();

        return services;
    }
}
