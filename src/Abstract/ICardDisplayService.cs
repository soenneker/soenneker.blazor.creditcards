using Soenneker.Blazor.CreditCards.Dtos;

namespace Soenneker.Blazor.CreditCards.Abstract;

/// <summary>
/// Service for detecting card metadata and retrieving card display styling based on card number and attributes.
/// </summary>
public interface ICardDisplayService
{
    /// <summary>
    /// Detects the card type, issuer, and program from a raw card number.
    /// </summary>
    /// <param name="cardNumber">The credit or debit card number, possibly including non-digit characters.</param>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item><description><c>Type</c>: The card network type (e.g., "visa", "amex").</description></item>
    /// <item><description><c>Issuer</c>: The card issuer (e.g., "chase", "amex").</description></item>
    /// <item><description><c>Program</c>: The specific card program or product (e.g., "sapphire_reserve").</description></item>
    /// </list>
    /// </returns>
    (string Type, string Issuer, string Program) DetectCardType(string cardNumber);

    /// <summary>
    /// Retrieves the appropriate visual style for a given card type, issuer, and program.
    /// </summary>
    /// <param name="cardType">The type of the card (e.g., "visa", "amex").</param>
    /// <param name="issuer">The issuer of the card (e.g., "chase", "citi").</param>
    /// <param name="program">The specific program or product name (e.g., "freedom_flex").</param>
    /// <returns>
    /// A <see cref="CardStyle"/> object representing the display characteristics of the card.
    /// </returns>
    CardStyle GetCardStyle(string cardType, string issuer, string program);
}
