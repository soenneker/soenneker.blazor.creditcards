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
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the create operation.
    /// </summary>
    /// <param name="container">The container.</param>
    /// <param name="card">The card.</param>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Create(ElementReference container, ElementReference card, string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates card style.
    /// </summary>
    /// <param name="card">The card.</param>
    /// <param name="style">The style.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask UpdateCardStyle(ElementReference card, CardStyle style, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the destroy operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Destroy(string id, CancellationToken cancellationToken = default);
}
