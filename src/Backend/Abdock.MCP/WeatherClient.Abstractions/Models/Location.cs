namespace WeatherClient.Abstractions.Models;

public record Location
{
    public required string City { get; init; }
    public required string Region { get; init; }
    public required string Country { get; init; }
    public required double Longitude { get; init; }
    public required double Latitude { get; init; }
};