using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Blazor.CreditCards.Tests;

[Collection("Collection")]
public class CreditCardsInteropTests : FixturedUnitTest
{
    private readonly ICreditCardsInterop _blazorlibrary;

    public CreditCardsInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _blazorlibrary = Resolve<ICreditCardsInterop>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
