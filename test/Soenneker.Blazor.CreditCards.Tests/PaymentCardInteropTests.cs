using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.CreditCards.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class PaymentCardInteropTests : HostedUnitTest
{
    private readonly IPaymentCardInterop _blazorlibrary;

    public PaymentCardInteropTests(Host host) : base(host)
    {
        _blazorlibrary = Resolve<IPaymentCardInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}
