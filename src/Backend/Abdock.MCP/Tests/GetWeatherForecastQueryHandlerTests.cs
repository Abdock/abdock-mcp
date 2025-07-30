using System.Net;
using Application.CQRS.Queries.Forecast;
using Application.DTO.Mappers;
using Application.DTO.Requests.Forecast;
using Application.DTO.Responses.General.Enums;
using AutoFixture.Xunit2;
using FluentAssertions;
using UnitsNet;
using WeatherClient.Abstractions;
using WeatherClient.Abstractions.Enums;
using WeatherClient.Abstractions.Models;
using Moq;

namespace Tests;

public class GetWeatherForecastQueryHandlerTests
{
    private readonly Mock<IWeatherClient> _mockWeatherClient;
    private readonly GetWeatherForecastQueryHandler _handler;

    public GetWeatherForecastQueryHandlerTests()
    {
        _mockWeatherClient = new Mock<IWeatherClient>();
        var mapper = new ResponseMapper();
        _handler = new GetWeatherForecastQueryHandler(_mockWeatherClient.Object, mapper);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenWeatherClientReturnsErrorWithErrorResponse_ShouldReturnMappedErrorCode(
        GetWeatherForecastRequest request,
        ErrorResponse errorResponse)
    {
        // Arrange
        var query = new GetWeatherForecastQuery { Request = request };
        ApiResponse<IReadOnlyCollection<WeatherForecastDay>> apiResponse = errorResponse;

        _mockWeatherClient
            .Setup(x => x.GetWeatherForecastAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Response.Should().BeNull();
        _mockWeatherClient.Verify(x => x.GetWeatherForecastAsync(request.Query, request.Days, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineAutoData(HttpStatusCode.BadRequest, CustomStatusCode.InvalidRequest)]
    [InlineAutoData(HttpStatusCode.Unauthorized, CustomStatusCode.InternalError)]
    [InlineAutoData(HttpStatusCode.Forbidden, CustomStatusCode.InternalError)]
    [InlineAutoData(HttpStatusCode.InternalServerError, CustomStatusCode.Unknown)]
    public async Task Handle_WhenWeatherClientReturnsErrorWithoutErrorResponse_ShouldReturnMappedStatusCode(
        HttpStatusCode httpStatusCode,
        CustomStatusCode expectedStatusCode,
        GetWeatherForecastRequest request)
    {
        // Arrange
        var query = new GetWeatherForecastQuery { Request = request };
        ApiResponse<IReadOnlyCollection<WeatherForecastDay>> apiResponse = httpStatusCode;

        _mockWeatherClient
            .Setup(x => x.GetWeatherForecastAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(expectedStatusCode);
        result.Response.Should().BeNull();
        _mockWeatherClient.Verify(x => x.GetWeatherForecastAsync(request.Query, request.Days, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineAutoData(ErrorType.InvalidRequest, CustomStatusCode.InvalidRequest)]
    [InlineAutoData(ErrorType.Unauthorized, CustomStatusCode.InternalError)]
    [InlineAutoData(ErrorType.Forbidden, CustomStatusCode.InternalError)]
    [InlineAutoData(ErrorType.ApiError, CustomStatusCode.InternalError)]
    public async Task Handle_WhenWeatherClientReturnsSpecificErrorTypes_ShouldReturnCorrectStatusCode(
        ErrorType errorType,
        CustomStatusCode expectedStatusCode,
        GetWeatherForecastRequest request)
    {
        // Arrange
        var query = new GetWeatherForecastQuery { Request = request };
        var errorResponse = new ErrorResponse
        {
            ErrorType = errorType,
            Message = "Test error message"
        };
        ApiResponse<IReadOnlyCollection<WeatherForecastDay>> apiResponse = errorResponse;

        _mockWeatherClient
            .Setup(x => x.GetWeatherForecastAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(expectedStatusCode);
        result.Response.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WithRealWeatherForecastData_ShouldMapCorrectly()
    {
        // Arrange
        var request = new GetWeatherForecastRequest("London", 3);
        var query = new GetWeatherForecastQuery { Request = request };

        var dailyWeather = new DailyWeather
        {
            MaxTemperature = Temperature.FromDegreesCelsius(18),
            MinTemperature = Temperature.FromDegreesCelsius(12),
            AverageTemperature = Temperature.FromDegreesCelsius(15),
            MaxWindSpeed = Speed.FromKilometersPerHour(20),
            TotalPrecipitation = Length.FromMillimeters(2.5),
            AverageVisibility = Length.FromKilometers(10),
            AverageHumidity = RelativeHumidity.FromPercent(75),
            WillItRain = true,
            ChanceOfRain = Ratio.FromPercent(80),
            WillItSnow = false,
            ChanceOfSnow = Ratio.FromPercent(0),
            ConditionText = "Partly cloudy",
            ConditionIcon = "//cdn.weatherapi.com/weather/64x64/day/116.png",
            ConditionCode = 1003,
            UvIndex = 4
        };

        var astronomy = new Astronomy
        {
            Sunrise = new TimeOnly(6, 30),
            Sunset = new TimeOnly(19, 45),
            Moonrise = new TimeOnly(14, 20),
            Moonset = new TimeOnly(3, 15),
            MoonPhase = "Waxing Crescent",
            MoonIllumination = Ratio.FromPercent(25)
        };

        var hourlyWeather = new List<HourlyWeather>
        {
            new()
            {
                Time = DateTime.Today.AddHours(12),
                Temperature = Temperature.FromDegreesCelsius(16),
                FeelsLike = Temperature.FromDegreesCelsius(15),
                IsDay = true,
                ConditionText = "Partly cloudy",
                ConditionIcon = "//cdn.weatherapi.com/weather/64x64/day/116.png",
                ConditionCode = 1003,
                WindSpeed = Speed.FromKilometersPerHour(15),
                WindDirection = Angle.FromDegrees(270),
                WindDirectionCompass = "W",
                Pressure = Pressure.FromMillibars(1015),
                Precipitation = Length.FromMillimeters(0),
                Humidity = RelativeHumidity.FromPercent(70),
                CloudCover = Ratio.FromPercent(50),
                WindChill = Temperature.FromDegreesCelsius(14),
                HeatIndex = Temperature.FromDegreesCelsius(16),
                DewPoint = Temperature.FromDegreesCelsius(10),
                WillItRain = false,
                ChanceOfRain = Ratio.FromPercent(20),
                WillItSnow = false,
                ChanceOfSnow = Ratio.FromPercent(0),
                Visibility = Length.FromKilometers(10),
                GustSpeed = Speed.FromKilometersPerHour(25),
                UvIndex = 4
            }
        };

        var weatherForecast = new List<WeatherForecastDay>
        {
            new()
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Day = dailyWeather,
                Astronomy = astronomy,
                HourlyForecast = hourlyWeather
            }
        };

        ApiResponse<IReadOnlyCollection<WeatherForecastDay>> apiResponse = weatherForecast;

        _mockWeatherClient
            .Setup(x => x.GetWeatherForecastAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(CustomStatusCode.Ok);
        result.Response.Should().NotBeNull();
        result.Response.Should().HaveCount(1);
        
        var firstDay = result.Response!.First();
        firstDay.Date.Should().Be(DateOnly.FromDateTime(DateTime.Today));
        firstDay.Day.MaxTemperature.Should().Be("18 °C");
        firstDay.Day.MinTemperature.Should().Be("12 °C");
        firstDay.HourlyForecast.Should().HaveCount(1);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(15)]
    [InlineData(-1)]
    public async Task Handle_WithInvalidDaysParameter_ShouldThrowArgumentOutOfRangeException(int invalidDays)
    {
        // Arrange
        var request = new GetWeatherForecastRequest("London", invalidDays);
        var query = new GetWeatherForecastQuery { Request = request };

        _mockWeatherClient
            .Setup(x => x.GetWeatherForecastAsync(request.Query, request.Days, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentOutOfRangeException(nameof(invalidDays), "Days must be between 1 and 14"));

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _handler.Handle(query, CancellationToken.None));
    }
}