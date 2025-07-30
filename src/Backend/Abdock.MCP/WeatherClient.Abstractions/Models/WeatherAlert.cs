namespace WeatherClient.Abstractions.Models;

public record WeatherAlert
{
    public required string Headline { get; init; }
    public required string? MessageType { get; init; }
    public required string? Severity { get; init; }
    public required string? Urgency { get; init; }
    public required string? Areas { get; init; }
    public required string Category { get; init; }
    public required string? Certainty { get; init; }
    public required string Event { get; init; }
    public required string? Note { get; init; }
    public required DateTime? Effective { get; init; }
    public required DateTime? Expires { get; init; }
    public required string Description { get; init; }
    public required string Instruction { get; init; }
}