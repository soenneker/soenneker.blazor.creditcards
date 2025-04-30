using System.Collections.Generic;
using System.Text.RegularExpressions;
using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Blazor.CreditCards.Dtos;

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
        if (string.IsNullOrWhiteSpace(cardNumber))
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