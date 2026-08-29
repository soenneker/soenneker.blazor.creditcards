[![](https://img.shields.io/nuget/v/soenneker.blazor.creditcards.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.creditcards/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.creditcards/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.creditcards/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.creditcards.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.creditcards/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.creditcards/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.creditcards/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.creditcards/actions/workflows/codeql.yml)

# Soenneker.Blazor.CreditCards

An animated Blazor credit-card visualizer with network detection, masked saved-card display, custom styling, and front/back flipping.

This is a display component, not a payment field. It does not tokenize, validate with Luhn, encrypt, or submit card data. Use a PCI-compliant hosted payment control such as Stripe Elements for real payment entry.

![Credit card component](https://github.com/user-attachments/assets/b0b21f74-0ef0-4a46-9b87-cf68a5110d32)

## Installation and registration

```bash
dotnet add package Soenneker.Blazor.CreditCards
```

```csharp
using Soenneker.Blazor.CreditCards.Registrars;

builder.Services.AddCreditCardsInteropAsScoped();
```

## Display a saved card

Prefer the last-four API for production account screens:

```razor
<CreditCard @ref="_card"
            CardHolderName="Ada Lovelace"
            ExpiryDate="12/29"
            FlipEnabled="true" />

@code {
    private CreditCard? _card;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await _card!.SetLast4("4242", type: "visa");
    }
}
```

`SetLast4` requires exactly four digits. `ResetCardDetection()` exits saved-card mode so subsequent `CardNumber` updates can select the visual style again.

## Bind preview values

```razor
<CreditCard CardNumber="@cardNumber"
            CardHolderName="@cardholderName"
            ExpiryDate="@expiryDate"
            Cvc="@cvc"
            MaskSensitiveData="true"
            Type="@cardType"
            @ref="_card" />
```

`MaskSensitiveData` defaults to `true`: only the final four number digits are rendered, and the CVC is always replaced with bullets. Setting it to `false` renders supplied values into the page DOM and should be limited to test data and controlled demonstrations. Passing sensitive values through Blazor parameters still places them in application memory even when the display is masked; do not use this component as the owner of real PAN or CVC input.

When `Type` is blank, the component detects supported networks from a complete number after removing formatting characters. Supplying `Type` overrides detection. Detection identifies a visual network pattern only; it does not prove that a card number is valid or usable.

For input controls that update outside normal parameter binding, call:

```csharp
await _card!.OnAnyInput(cardNumber, cardholderName, expiryDate, cvc);
```

## Flip and click behavior

With no `OnClick` handler, clicking flips the card when `FlipEnabled` is `true`. Supplying `OnClick` replaces that default behavior; call `Flip()` inside the handler if the click should still flip it.

```razor
<CreditCard OnClick="HandleClick" @ref="_card" />

@code {
    private void HandleClick(MouseEventArgs _)
    {
        _card?.Flip();
    }
}
```

Brand artwork is loaded from jsDelivr. Allow `https://cdn.jsdelivr.net` in the image policy when using a Content Security Policy. The component removes its JavaScript observer when disposed.
