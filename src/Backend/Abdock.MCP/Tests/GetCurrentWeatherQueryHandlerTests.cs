using System.Net;
using Application.CQRS.Queries.Weather;
using Application.DTO.Mappers;
using Application.DTO.Requests.Weather;
using Application.DTO.Responses.General.Enums;
using AutoFixture.Xunit2;
using FluentAssertions;
using UnitsNet;
using WeatherClient.Abstractions;
using WeatherClient.Abstractions.Enums;
using WeatherClient.Abstractions.Models;
using Moq;

namespace Tests;

public class GetCurrentWeatherQueryHandlerTests
{
    private readonly Mock<IWeatherClient> _mockWeatherClient;
    private readonly GetCurrentWeatherQueryHandler _handler;

    public GetCurrentWeatherQueryHandlerTests()
    {
        _mockWeatherClient = new Mock<IWeatherClient>();
        var mapper = new ResponseMapper();
        _handler = new GetCurrentWeatherQueryHandler(_mockWeatherClient.Object, mapper);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenWeatherClientReturnsSuccess_ShouldReturnMappedResponse(
        GetCurrentWeatherRequest request,
        Weather weather)
    {
        // Arrange
        var query = new GetCurrentWeatherQuery { Request = request };
        ApiResponse<Weather> apiResponse = weather;

        _mockWeatherClient
            .Setup(x => x.GeCurrentWeatherAsync(request.Query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(CustomStatusCode.Ok);
        result.Response.Should().NotBeNull();
        result.Response!.Location.Should().NotBeNull();
        result.Response.Temperature.Should().NotBeNullOrEmpty();

        _mockWeatherClient.Verify(x => x.GeCurrentWeatherAsync(request.Query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenWeatherClientReturnsErrorWithErrorResponse_ShouldReturnMappedErrorCode(
        GetCurrentWeatherRequest request,
        ErrorResponse errorResponse)
    {
        // Arrange
        var query = new GetCurrentWeatherQuery { Request = request };
        ApiResponse<Weather> apiResponse = errorResponse;

        _mockWeatherClient
            .Setup(x => x.GeCurrentWeatherAsync(request.Query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Response.Should().BeNull();
        _mockWeatherClient.Verify(x => x.GeCurrentWeatherAsync(request.Query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineAutoData(HttpStatusCode.BadRequest, CustomStatusCode.InvalidRequest)]
    [InlineAutoData(HttpStatusCode.Unauthorized, CustomStatusCode.InternalError)]
    [InlineAutoData(HttpStatusCode.Forbidden, CustomStatusCode.InternalError)]
    [InlineAutoData(HttpStatusCode.InternalServerError, CustomStatusCode.Unknown)]
    public async Task Handle_WhenWeatherClientReturnsErrorWithoutErrorResponse_ShouldReturnMappedStatusCode(
        HttpStatusCode httpStatusCode,
        CustomStatusCode expectedStatusCode,
        GetCurrentWeatherRequest request)
    {
        // Arrange
        var query = new GetCurrentWeatherQuery { Request = request };
        ApiResponse<Weather> apiResponse = httpStatusCode;

        _mockWeatherClient
            .Setup(x => x.GeCurrentWeatherAsync(request.Query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(expectedStatusCode);
        result.Response.Should().BeNull();
        _mockWeatherClient.Verify(x => x.GeCurrentWeatherAsync(request.Query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineAutoData(ErrorType.InvalidRequest, CustomStatusCode.InvalidRequest)]
    [InlineAutoData(ErrorType.Unauthorized, CustomStatusCode.InternalError)]
    [InlineAutoData(ErrorType.Forbidden, CustomStatusCode.InternalError)]
    [InlineAutoData(ErrorType.ApiError, CustomStatusCode.InternalError)]
    public async Task Handle_WhenWeatherClientReturnsSpecificErrorTypes_ShouldReturnCorrectStatusCode(
        ErrorType errorType,
        CustomStatusCode expectedStatusCode,
        GetCurrentWeatherRequest request)
    {
        // Arrange
        var query = new GetCurrentWeatherQuery { Request = request };
        var errorResponse = new ErrorResponse
        {
            ErrorType = errorType,
            Message = "Test error message"
        };
        ApiResponse<Weather> apiResponse = errorResponse;

        _mockWeatherClient
            .Setup(x => x.GeCurrentWeatherAsync(request.Query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(expectedStatusCode);
        result.Response.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WithRealWeatherData_ShouldMapCorrectly()
    {
        // Arrange
        var request = new GetCurrentWeatherRequest("New York");
        var query = new GetCurrentWeatherQuery { Request = request };

        var location = new Location
        {
            City = "New York",
            Region = "New York",
            Country = "United States of America",
            Latitude = 40.7589,
            Longitude = -73.9851
        };

        var weather = new Weather
        {
            Location = location,
            Temperature = Temperature.FromDegreesCelsius(25),
            FeelsLike = Temperature.FromDegreesCelsius(27),
            WindSpeed = Speed.FromKilometersPerHour(15),
            Pressure = Pressure.FromMillibars(1013)
        };

        ApiResponse<Weather> apiResponse = weather;

        _mockWeatherClient
            .Setup(x => x.GeCurrentWeatherAsync(request.Query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(CustomStatusCode.Ok);
        result.Response.Should().NotBeNull();
        result.Response!.Location.City.Should().Be("New York");
        result.Response.Location.Country.Should().Be("United States of America");
        result.Response.Temperature.Should().Be("25 °C");
    }
}