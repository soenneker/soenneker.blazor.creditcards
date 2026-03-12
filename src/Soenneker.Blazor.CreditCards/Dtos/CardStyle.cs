using System.Text.Json.Serialization;

namespace Soenneker.Blazor.CreditCards.Dtos;

public sealed class CardStyle
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("gradient")]
    public string? Gradient { get; set; }

    [JsonPropertyName("backgroundColor")]
    public string? BackgroundColor { get; set; }

    [JsonPropertyName("pattern")]
    public string? Pattern { get; set; }

    [JsonPropertyName("logoPosition")]
    public string LogoPosition { get; set; } = "right";

    [JsonPropertyName("logoSize")]
    public string LogoSize { get; set; } = "100px 60px";

    [JsonPropertyName("hasChip")]
    public bool HasChip { get; set; }

    [JsonPropertyName("hasContactless")]
    public bool HasContactless { get; set; }

    [JsonPropertyName("hasHologram")]
    public bool HasHologram { get; set; }
}