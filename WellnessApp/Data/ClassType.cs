namespace Lumina.Data;

/// <summary>
/// A type of class offered at Lumina (e.g. Reformer Pilates, Restorative Yoga).
/// Rich metadata for accessibility and member discovery.
/// </summary>
public class ClassType
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int DurationMinutes { get; set; } = 60;
    public int DefaultCapacity { get; set; } = 12;

    /// <summary>
    /// Beginner, Intermediate, AllLevels, etc.
    /// </summary>
    public string Level { get; set; } = "All Levels";

    /// <summary>
    /// Comma or JSON for modalities (Pilates, Yoga, Barre, Meditation...)
    /// </summary>
    public string Modalities { get; set; } = string.Empty;

    /// <summary>
    /// Detailed accessibility & modification notes.
    /// This is what makes Lumina special.
    /// </summary>
    public string AccessFeatures { get; set; } = string.Empty;

    public string WhatToExpect { get; set; } = string.Empty;
    public string WhatToBring { get; set; } = string.Empty;

    public string? AccentColor { get; set; } // For beautiful UI cards

    public bool IsActive { get; set; } = true;
}