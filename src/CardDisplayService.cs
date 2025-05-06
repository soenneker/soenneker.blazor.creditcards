using System.Collections.Generic;
using System.Text.RegularExpressions;
using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Blazor.CreditCards.Dtos;
using Soenneker.Extensions.String;

namespace Soenneker.Blazor.CreditCards;

public sealed class CardDisplayService : ICardDisplayService
{
    private static readonly Dictionary<string, (string Pattern, string Type, string Issuer, string Program)> _binPatterns = new()
    {
        // Visa
        {"visa", ("^4[0-9]{12}(?:[0-9]{3})?$", "visa", "visa", "standard")},

        // MasterCard
        {"mastercard", ("^(5[1-5][0-9]{14}|2(2[2-9][0-9]{12}|[3-6][0-9]{13}|7[01][0-9]{12}|720[0-9]{12}))$", "mastercard", "mastercard", "standard")},

        // American Express
        {"amex", ("^3[47][0-9]{13}$", "amex", "amex", "standard")},

        // Discover
        {"discover", ("^6(?:011|5[0-9]{2}|4[4-9][0-9])[0-9]{12}$", "discover", "discover", "standard")},

        // JCB
        {"jcb", ("^(?:2131|1800|35\\d{3})\\d{11}$", "jcb", "jcb", "standard")},

        // Diners Club
        {"diners", ("^3(?:0[0-5]|[68][0-9])[0-9]{11}$", "diners", "diners", "standard")},

        // UnionPay
        {"unionpay", ("^62[0-9]{14,17}$", "unionpay", "unionpay", "standard")},

        // Maestro (common in Europe, often 12-19 digits)
        {"maestro", ("^(5018|5020|5038|56|58|6304|6759|6761|6762|6763)[0-9]{8,15}$", "maestro", "maestro", "standard")},

        // Elo (Brazil)
        {"elo", ("^(4011(78|79)|4312(74|75)|4389(35|36)|4514(16|17)|4576(31|32)|4576(51|52)|5041(75|76)|5067(0[0-9]|1[0-9]|20)|5090(4[0-9]|5[0-9]|6[0-9]|7[0-9]|8[0-9])|6277(00|01|02)|6363(68|69)|6500(31|32|33)|6500(51|52)|6504(84|85)|6504(91|92)|6505(03|04)|6516(52|53)|6550(00|01))\\d*$", "elo", "elo", "standard")},

        // Mir (Russia)
        {"mir", ("^220[0-4][0-9]{12}$", "mir", "mir", "standard")},

        // Hipercard (Brazil)
        {"hipercard", ("^(3841(0[0-9]|1[0-9]|2[0-9])|60[0-9]{14})$", "hipercard", "hipercard", "standard")},

        // Carte Bancaire (France, overlaps with Visa and Mastercard)
        {"cartebancaire", ("^((4[0-9]{12}(?:[0-9]{3})?)|(5[1-5][0-9]{14}))$", "visa-mastercard", "cartebancaire", "standard")},
    };

    private static readonly Dictionary<string, CardStyle> _cardStyles = new()
    {
        {
            "visa_standard", new CardStyle
            {
                Gradient ="linear-gradient(135deg, #2b6edc, #7ca8f8)", // deep royal blues
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "mastercard_standard", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #000000, #434343)", // true black/charcoal
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "amex_standard", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #016fd0, #70bdf0)", // vibrant Amex blue
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "discover_standard", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #b7aead, #f1ece8)", // bright platinum white
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "jcb_standard", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #002d62, #4ba3ff)", // electric navy
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "diners_standard", new CardStyle
            {
                Gradient = "linear-gradient(135deg, #444444, #cccccc)", // brushed steel
                Pattern = "none",
                LogoPosition = "right",
                LogoSize = "100px 60px"
            }
        },
        {
            "unionpay_standard", new CardStyle
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

        // Remove any non-digit characters
        cardNumber = Regex.Replace(cardNumber, @"[^\d]", "");

        foreach (KeyValuePair<string, (string Pattern, string Type, string Issuer, string Program)> pattern in _binPatterns)
        {
            if (Regex.IsMatch(cardNumber, pattern.Value.Pattern))
            {
                return (pattern.Value.Type, pattern.Value.Issuer, pattern.Value.Program);
            }
        }

        return ("unknown", "standard", "standard");
    }

    public CardStyle GetCardStyle(string cardType, string issuer, string program)
    {
        var key = $"{cardType}_{issuer}_{program}";
        if (_cardStyles.TryGetValue(key, out CardStyle? style))
        {
            style.Type = cardType;
            return style;
        }

        key = $"{cardType}_{issuer}";
        if (_cardStyles.TryGetValue(key, out style))
        {
            style.Type = cardType;
            return style;
        }

        key = $"{cardType}_standard";
        if (_cardStyles.TryGetValue(key, out style))
        {
            style.Type = cardType;
            return style;
        }

        return new CardStyle
        {
            Type = cardType,
            Gradient = "linear-gradient(135deg, #666, #999)",
            Pattern = "none",
            LogoPosition = "right",
            LogoSize = "100px 60px"
        };
    }
}