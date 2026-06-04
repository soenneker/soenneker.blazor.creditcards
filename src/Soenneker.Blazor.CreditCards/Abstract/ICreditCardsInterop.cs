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
    ValueTask Initialize(CancellationToken cancellationToken = default);

    ValueTask Create(ElementReference container, ElementReference card, string id,
        CancellationToken cancellationToken = default);

    ValueTask UpdateCardStyle(ElementReference card, CardStyle style, CancellationToken cancellationToken = default);

    ValueTask Destroy(string id, CancellationToken cancellationToken = default);
}
