using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Blazor.CreditCards.Dtos;
using Soenneker.Extensions.String;

namespace Soenneker.Blazor.CreditCards;

/// <inheritdoc cref="ICardDisplayService"/>
public sealed partial class CardDisplayService : ICardDisplayService
{
    private static readonly (Regex Pattern, string Type, string Issuer, string Program)[] _binPatterns =
    [
        // Visa
        (CardPattern0(), "visa", "visa", "standard"),

        // MasterCard
        (CardPattern1(), "mastercard", "mastercard", "standard"),

        // American Express
        (CardPattern2(), "amex", "amex", "standard"),

        // Discover
        (CardPattern3(), "discover", "discover", "standard"),

        // JCB
        (CardPattern4(), "jcb", "jcb", "standard"),

        // Diners Club
        (CardPattern5(), "diners", "diners", "standard"),

        // UnionPay
        (CardPattern6(), "unionpay", "unionpay", "standard"),

        // Maestro (common in Europe, often 12-19 digits)
        (CardPattern7(), "maestro", "maestro", "standard"),

        // Elo (Brazil)
        (CardPattern8(), "elo", "elo", "standard"),

        // Mir (Russia)
        (CardPattern9(), "mir", "mir", "standard"),

        // Hipercard (Brazil)
        (CardPattern10(), "hipercard", "hipercard", "standard"),

        // Carte Bancaire (France, overlaps with Visa and Mastercard)
        (CardPattern11(), "visa-mastercard", "cartebancaire", "standard"),
    ];

    private static readonly Dictionary<string, CardStyle> _cardStyles = new()
    {
        {
            "visa", new CardStyle
            {
                Gradient ="linear-gradient(135deg, #2b6edc, #7ca8f8)", // deep royal blues
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "mastercard", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #000000, #434343)", // true black/charcoal
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "amex", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #016fd0, #70bdf0)", // vibrant Amex blue
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "discover", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #b7aead, #f1ece8)", // bright platinum white
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "jcb", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #002d62, #4ba3ff)", // electric navy
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "diners", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #444444, #cccccc)", // brushed steel
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "unionpay", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #005d8f, #7dd2fc)", // brighter contrast UnionPay blue
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
    };


    public (string Type, string Issuer, string Program) DetectCardType(string cardNumber)
    {
        if (cardNumber.IsNullOrWhiteSpace())
            return ("unknown", "standard", "standard");

        if (cardNumber.Length > 64)
            return ("unknown", "standard", "standard");

        Span<char> digits = stackalloc char[64];
        var count = 0;

        foreach (char character in cardNumber)
        {
            if (char.IsDigit(character))
                digits[count++] = character;
        }

        ReadOnlySpan<char> number = digits[..count];

        foreach (var pattern in _binPatterns)
        {
            if (pattern.Pattern.IsMatch(number))
                return (pattern.Type, pattern.Issuer, pattern.Program);
        }

        return ("unknown", "standard", "standard");
    }

    public CardStyle GetCardStyle(string cardType, string issuer, string program)
    {
        if (_cardStyles.TryGetValue(cardType, out CardStyle? style))
            return CopyStyle(style, cardType);

        return new CardStyle
        {
            Type = cardType,
            Gradient = "linear-gradient(135deg, #666, #999)",
            Pattern = "none",
            LogoPosition = "right",
            LogoSize = "100px 60px"
        };
    }

    private static CardStyle CopyStyle(CardStyle style, string cardType)
    {
        return new CardStyle
        {
            Type = cardType,
            Gradient = style.Gradient,
            BackgroundColor = style.BackgroundColor,
            Pattern = style.Pattern,
            LogoPosition = style.LogoPosition,
            LogoSize = style.LogoSize,
            HasChip = style.HasChip,
            HasContactless = style.HasContactless,
            HasHologram = style.HasHologram
        };
    }

    [GeneratedRegex("^4[0-9]{12}(?:[0-9]{3})?$")]
    private static partial Regex CardPattern0();

    [GeneratedRegex("^(5[1-5][0-9]{14}|2(2[2-9][0-9]{12}|[3-6][0-9]{13}|7[01][0-9]{12}|720[0-9]{12}))$")]
    private static partial Regex CardPattern1();

    [GeneratedRegex("^3[47][0-9]{13}$")]
    private static partial Regex CardPattern2();

    [GeneratedRegex("^6(?:011|5[0-9]{2}|4[4-9][0-9])[0-9]{12}$")]
    private static partial Regex CardPattern3();

    [GeneratedRegex("^(?:2131|1800|35\\d{3})\\d{11}$")]
    private static partial Regex CardPattern4();

    [GeneratedRegex("^3(?:0[0-5]|[68][0-9])[0-9]{11}$")]
    private static partial Regex CardPattern5();

    [GeneratedRegex("^62[0-9]{14,17}$")]
    private static partial Regex CardPattern6();

    [GeneratedRegex("^(5018|5020|5038|56|58|6304|6759|6761|6762|6763)[0-9]{8,15}$")]
    private static partial Regex CardPattern7();

    [GeneratedRegex("^(4011(78|79)|4312(74|75)|4389(35|36)|4514(16|17)|4576(31|32)|4576(51|52)|5041(75|76)|5067(0[0-9]|1[0-9]|20)|5090(4[0-9]|5[0-9]|6[0-9]|7[0-9]|8[0-9])|6277(00|01|02)|6363(68|69)|6500(31|32|33)|6500(51|52)|6504(84|85)|6504(91|92)|6505(03|04)|6516(52|53)|6550(00|01))\\d*$")]
    private static partial Regex CardPattern8();

    [GeneratedRegex("^220[0-4][0-9]{12}$")]
    private static partial Regex CardPattern9();

    [GeneratedRegex("^(3841(0[0-9]|1[0-9]|2[0-9])|60[0-9]{14})$")]
    private static partial Regex CardPattern10();

    [GeneratedRegex("^((4[0-9]{12}(?:[0-9]{3})?)|(5[1-5][0-9]{14}))$")]
    private static partial Regex CardPattern11();
}
