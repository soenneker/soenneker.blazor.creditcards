[![](https://img.shields.io/nuget/v/soenneker.blazor.creditcards.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.creditcards/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.creditcards/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.creditcards/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.creditcards.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.creditcards/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.creditcards/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.CreditCards

### A beautiful, animated credit card component with real-time updates and brand detection

![image](https://github.com/user-attachments/assets/b0b21f74-0ef0-4a46-9b87-cf68a5110d32)

## Installation

```
dotnet add package Soenneker.Blazor.CreditCards
```

---

### ✨ Features

- 💳 Live-updating, animated card rendering
- 🧠 Automatic card type detection (Visa, Mastercard, Amex, etc.)
- 🖼️ Built-in front/back flip animation
- 🖱️ Click event support for interactive behaviors
- 🧼 Placeholder logic for empty cards
- 🧪 Perfect for forms, payment demos, and simulations

---

## 📦 Installation

```bash
dotnet add package Soenneker.Blazor.CreditCards
```

---

## 🛠️ Usage

### 1. Register the interop service

```csharp
builder.Services.AddCreditCardsInteropAsScoped();
```

### 2. Add the component

```razor
<CreditCard CardNumber="@CardNumber"
            CardholderName="@CardholderName"
            ExpiryDate="@ExpiryDate"
            Cvc="@Cvc"
            FlipEnabled="true"
            OnClick="HandleCardClick"
            @ref="_creditCard" />

### 3. Handle click events (optional)

```csharp
private async Task HandleCardClick(MouseEventArgs args)
{
    // Example: Flip the card when clicked
    _creditCard?.Flip();
    
    // Or perform any other action
    Console.WriteLine($"Card clicked at: {args.ClientX}, {args.ClientY}");
}
```

### 4. Control flip functionality

```razor
<!-- Enable/disable flip functionality -->
<CreditCard FlipEnabled="false" ... />

<!-- Default behavior: flip is enabled -->
<CreditCard FlipEnabled="true" ... />
```

**FlipEnabled Parameter:**
- `true` (default): Card can be flipped by clicking or programmatically
- `false`: Disables flip functionality, cursor changes to default, and Flip() method does nothing

### 5. Programmatic card control

```csharp
// Flip the card programmatically
_creditCard?.Flip();

// Set last 4 digits only (for saved cards)
await _creditCard?.SetLast4("1234", "visa");

// Reset to full input mode
_creditCard?.ResetCardDetection();
```
```
