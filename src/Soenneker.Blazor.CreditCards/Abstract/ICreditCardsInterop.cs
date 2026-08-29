using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.CreditCards.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.CreditCards.Abstract;

/// <summary>
/// A lightweight Blazor library for realistic, customizable credit and debit card displays with BIN-based styling, issuer detection, and full support for branding and card metadata visualization.
/// </summary>
public interface ICreditCardsInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the credit cards so it is ready for use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the credit cards is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a credit cards instance from the supplied inputs.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="card">Element used to host the card input.</param>
    /// <param name="id">Identifier of the credit cards instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the create operation is complete.</returns>
    ValueTask Create(ElementReference container, ElementReference card, string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates card style.
    /// </summary>
    /// <param name="card">Element used to host the card input.</param>
    /// <param name="style">Style for the update card style operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the card style update is complete.</returns>
    ValueTask UpdateCardStyle(ElementReference card, CardStyle style, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the resources held by the credit cards.
    /// </summary>
    /// <param name="id">Identifier of the credit cards instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the destroy operation is complete.</returns>
    ValueTask Destroy(string id, CancellationToken cancellationToken = default);
}
