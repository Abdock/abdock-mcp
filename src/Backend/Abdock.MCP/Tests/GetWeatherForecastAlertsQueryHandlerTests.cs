using System.Net;
using Application.CQRS.Queries.Alerts;
using Application.DTO.Mappers;
using Application.DTO.Requests.Alert;
using Application.DTO.Responses.General.Enums;
using AutoFixture.Xunit2;
using FluentAssertions;
using WeatherClient.Abstractions;
using WeatherClient.Abstractions.Enums;
using WeatherClient.Abstractions.Models;
using Moq;

namespace Tests;

public class GetWeatherForecastAlertsQueryHandlerTests
{
    private readonly Mock<IWeatherClient> _mockWeatherClient;
    private readonly GetWeatherForecastAlertsQueryHandler _handler;

    public GetWeatherForecastAlertsQueryHandlerTests()
    {
        _mockWeatherClient = new Mock<IWeatherClient>();
        var mapper = new ResponseMapper();
        _handler = new GetWeatherForecastAlertsQueryHandler(_mockWeatherClient.Object, mapper);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenWeatherClientReturnsSuccess_ShouldReturnMappedResponse(
        GetWeatherForecastAlertsRequest request,
        WeatherAlert[] weatherAlerts)
    {
        // Arrange
        var query = new GetWeatherForecastAlertsQuery { Request = request };
        ApiResponse<IReadOnlyCollection<WeatherAlert>> apiResponse = weatherAlerts;

        _mockWeatherClient
            .Setup(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(CustomStatusCode.Ok);
        result.Response.Should().NotBeNull();
        result.Response.Should().HaveCount(weatherAlerts.Length);

        _mockWeatherClient.Verify(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenWeatherClientReturnsEmptyAlerts_ShouldReturnEmptyCollection(
        GetWeatherForecastAlertsRequest request)
    {
        // Arrange
        var query = new GetWeatherForecastAlertsQuery { Request = request };
        var emptyAlerts = Array.Empty<WeatherAlert>();
        ApiResponse<IReadOnlyCollection<WeatherAlert>> apiResponse = emptyAlerts;

        _mockWeatherClient
            .Setup(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(CustomStatusCode.Ok);
        result.Response.Should().NotBeNull();
        result.Response.Should().BeEmpty();

        _mockWeatherClient.Verify(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenWeatherClientReturnsErrorWithErrorResponse_ShouldReturnMappedErrorCode(
        GetWeatherForecastAlertsRequest request,
        ErrorResponse errorResponse)
    {
        // Arrange
        var query = new GetWeatherForecastAlertsQuery { Request = request };
        ApiResponse<IReadOnlyCollection<WeatherAlert>> apiResponse = errorResponse;

        _mockWeatherClient
            .Setup(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Response.Should().BeNull();
        _mockWeatherClient.Verify(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineAutoData(HttpStatusCode.BadRequest, CustomStatusCode.InvalidRequest)]
    [InlineAutoData(HttpStatusCode.Unauthorized, CustomStatusCode.InternalError)]
    [InlineAutoData(HttpStatusCode.Forbidden, CustomStatusCode.InternalError)]
    [InlineAutoData(HttpStatusCode.InternalServerError, CustomStatusCode.Unknown)]
    public async Task Handle_WhenWeatherClientReturnsErrorWithoutErrorResponse_ShouldReturnMappedStatusCode(
        HttpStatusCode httpStatusCode,
        CustomStatusCode expectedStatusCode,
        GetWeatherForecastAlertsRequest request)
    {
        // Arrange
        var query = new GetWeatherForecastAlertsQuery { Request = request };
        ApiResponse<IReadOnlyCollection<WeatherAlert>> apiResponse = httpStatusCode;

        _mockWeatherClient
            .Setup(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(expectedStatusCode);
        result.Response.Should().BeNull();
        _mockWeatherClient.Verify(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineAutoData(ErrorType.InvalidRequest, CustomStatusCode.InvalidRequest)]
    [InlineAutoData(ErrorType.Unauthorized, CustomStatusCode.InternalError)]
    [InlineAutoData(ErrorType.Forbidden, CustomStatusCode.InternalError)]
    [InlineAutoData(ErrorType.ApiError, CustomStatusCode.InternalError)]
    public async Task Handle_WhenWeatherClientReturnsSpecificErrorTypes_ShouldReturnCorrectStatusCode(
        ErrorType errorType,
        CustomStatusCode expectedStatusCode,
        GetWeatherForecastAlertsRequest request)
    {
        // Arrange
        var query = new GetWeatherForecastAlertsQuery { Request = request };
        var errorResponse = new ErrorResponse
        {
            ErrorType = errorType,
            Message = "Test error message"
        };
        ApiResponse<IReadOnlyCollection<WeatherAlert>> apiResponse = errorResponse;

        _mockWeatherClient
            .Setup(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(expectedStatusCode);
        result.Response.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WithRealWeatherAlertData_ShouldMapCorrectly()
    {
        // Arrange
        var request = new GetWeatherForecastAlertsRequest("Miami", 2);
        var query = new GetWeatherForecastAlertsQuery { Request = request };

        var weatherAlerts = new List<WeatherAlert>
        {
            new()
            {
                Headline = "Hurricane Warning issued for Miami-Dade County",
                MessageType = "Alert",
                Severity = "Severe",
                Urgency = "Immediate",
                Areas = "Miami-Dade County",
                Category = "Met",
                Certainty = "Likely",
                Event = "Hurricane Warning",
                Note = "Monitor conditions closely",
                Effective = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(2),
                Description = "A hurricane warning means that hurricane conditions are expected somewhere within the warning area within 36 hours.",
                Instruction = "Complete preparations immediately. Evacuate if directed by local authorities."
            },
            new()
            {
                Headline = "Storm Surge Warning for coastal areas",
                MessageType = "Warning",
                Severity = "Moderate",
                Urgency = "Expected",
                Areas = "Coastal Miami-Dade",
                Category = "Met",
                Certainty = "Possible",
                Event = "Storm Surge Warning",
                Note = "Coastal flooding expected",
                Effective = DateTime.UtcNow.AddHours(6),
                Expires = DateTime.UtcNow.AddDays(1),
                Description = "Storm surge will cause significant coastal flooding in low-lying areas.",
                Instruction = "Move to higher ground immediately if in evacuation zones."
            }
        };

        ApiResponse<IReadOnlyCollection<WeatherAlert>> apiResponse = weatherAlerts;

        _mockWeatherClient
            .Setup(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(CustomStatusCode.Ok);
        result.Response.Should().NotBeNull();
        result.Response.Should().HaveCount(2);
        
        var firstAlert = result.Response!.First();
        firstAlert.Headline.Should().Be("Hurricane Warning issued for Miami-Dade County");
        firstAlert.Event.Should().Be("Hurricane Warning");
        firstAlert.Severity.Should().Be("Severe");
        firstAlert.Category.Should().Be("Met");
        
        var secondAlert = result.Response!.Skip(1).First();
        secondAlert.Headline.Should().Be("Storm Surge Warning for coastal areas");
        secondAlert.Event.Should().Be("Storm Surge Warning");
        secondAlert.Severity.Should().Be("Moderate");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(15)]
    [InlineData(-1)]
    public async Task Handle_WithInvalidDaysParameter_ShouldThrowArgumentOutOfRangeException(int invalidDays)
    {
        // Arrange
        var request = new GetWeatherForecastAlertsRequest("Miami", invalidDays);
        var query = new GetWeatherForecastAlertsQuery { Request = request };

        _mockWeatherClient
            .Setup(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentOutOfRangeException(nameof(invalidDays), "Days must be between 1 and 14"));

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _handler.Handle(query, CancellationToken.None));
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenCancellationRequested_ShouldPassCancellationToken(
        GetWeatherForecastAlertsRequest request)
    {
        // Arrange
        var query = new GetWeatherForecastAlertsQuery { Request = request };
        var cancellationToken = new CancellationToken(true);
        var emptyAlerts = Array.Empty<WeatherAlert>();
        ApiResponse<IReadOnlyCollection<WeatherAlert>> apiResponse = emptyAlerts;

        _mockWeatherClient
            .Setup(x => x.GetWeatherAlertsAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockWeatherClient.Verify(x => x.GetWeatherAlertsAsync(request.Query, request.Days, cancellationToken), Times.Once);
    }
}