[![](https://img.shields.io/nuget/v/soenneker.blazor.creditcards.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.creditcards/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.creditcards/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.creditcards/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.creditcards.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.creditcards/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.CreditCards

> 🧪 **[Click here to try the demo](https://soenneker.github.io/soenneker.blazor.creditcards)**

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
            @ref="_creditCard" />
```
