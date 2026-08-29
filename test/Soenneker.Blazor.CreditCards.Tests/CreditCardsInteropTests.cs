using AwesomeAssertions;
using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.CreditCards.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class CreditCardsInteropTests : HostedUnitTest
{
    private readonly ICreditCardsInterop _blazorlibrary;
    private readonly ICardDisplayService _displayService;

    public CreditCardsInteropTests(Host host) : base(host)
    {
        _blazorlibrary = Resolve<ICreditCardsInterop>(true);
        _displayService = Resolve<ICardDisplayService>(true);
    }

    [Test]
    public void Card_styles_are_independent()
    {
        var first = _displayService.GetCardStyle("visa", "visa", "standard");
        first.Gradient = "changed";

        var second = _displayService.GetCardStyle("visa", "visa", "standard");

        second.Gradient.Should().NotBe("changed");
    }

    [Test]
    public void Oversized_card_number_is_not_detected()
    {
        var result = _displayService.DetectCardType(new string('4', 65));

        result.Type.Should().Be("unknown");
    }
}
