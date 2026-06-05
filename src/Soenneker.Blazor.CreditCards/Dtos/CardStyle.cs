using System.Text.Json.Serialization;

namespace Soenneker.Blazor.CreditCards.Dtos;

/// <summary>
/// Represents the card style.
/// </summary>
public sealed class CardStyle
{
    /// <summary>
    /// Gets or sets type.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets gradient.
    /// </summary>
    [JsonPropertyName("gradient")]
    public string? Gradient { get; set; }

    /// <summary>
    /// Gets or sets background color.
    /// </summary>
    [JsonPropertyName("backgroundColor")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets pattern.
    /// </summary>
    [JsonPropertyName("pattern")]
    public string? Pattern { get; set; }

    /// <summary>
    /// Gets or sets logo position.
    /// </summary>
    [JsonPropertyName("logoPosition")]
    public string LogoPosition { get; set; } = "right";

    /// <summary>
    /// Gets or sets logo size.
    /// </summary>
    [JsonPropertyName("logoSize")]
    public string LogoSize { get; set; } = "100px 60px";

    /// <summary>
    /// Gets or sets a value indicating whether the instance has chip.
    /// </summary>
    [JsonPropertyName("hasChip")]
    public bool HasChip { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the instance has contactless.
    /// </summary>
    [JsonPropertyName("hasContactless")]
    public bool HasContactless { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the instance has hologram.
    /// </summary>
    [JsonPropertyName("hasHologram")]
    public bool HasHologram { get; set; }
}