using System.Threading;
using System.Threading.Tasks;
using Soenneker.Quark.Components.Cancellable.Abstract;

namespace Soenneker.Blazor.CreditCards.Abstract;

/// <summary>
/// Represents the interactive visual credit card component interface for use with Blazor.
/// </summary>
public interface ICreditCardComponent : ICancellableComponent
{
    /// <summary>
    /// Updates the card visuals when input fields change.
    /// </summary>
    /// <param name="cardNumber">The full or partial card number entered by the user.</param>
    /// <param name="cardholderName">The name on the card.</param>
    /// <param name="expiryDate">The expiry date of the card (MM/YY format).</param>
    /// <param name="cvc">The CVC/CVV security code.</param>
    /// <returns>A task that completes when the component updates.</returns>
    Task OnAnyInput(string cardNumber, string cardholderName, string expiryDate, string cvc);

    /// <summary>
    /// Flips the card between front and back visual state.
    /// </summary>
    void Flip();

    /// <summary>
    /// Sets the last four digits of the card for display, masking the rest.
    /// This also allows manually specifying the card type, issuer, and program.
    /// </summary>
    /// <param name="last4">The last 4 digits of the card.</param>
    /// <param name="type">The card type (e.g., visa, amex).</param>
    /// <param name="issuer">The card issuer name or classification.</param>
    /// <param name="program">The card program or level.</param>
    /// <param name="cancellationToken"></param>
    ValueTask SetLast4(string last4, string type = "unknown", string issuer = "standard", string program = "standard", CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears the "last 4 only" display mode and enables full input detection.
    /// </summary>
    void ResetCardDetection();
}
