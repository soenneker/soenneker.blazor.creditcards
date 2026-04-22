using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.CreditCards.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class CreditCardsInteropTests : HostedUnitTest
{
    private readonly ICreditCardsInterop _blazorlibrary;

    public CreditCardsInteropTests(Host host) : base(host)
    {
        _blazorlibrary = Resolve<ICreditCardsInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}
