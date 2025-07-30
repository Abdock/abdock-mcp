namespace Application.DTO.Responses.Location;

public record LocationResponse
{
    public required string City { get; init; }
    public required string Region { get; init; }
    public required string Country { get; init; }
    public required double Longitude { get; init; }
    public required double Latitude { get; init; }
}