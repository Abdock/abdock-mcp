using UnitsNet;
using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.DTO;

namespace WeatherClient.WeatherApi.Mapping;

internal static class WeatherMapping
{
    public static Weather MapToWeather(this CurrentWeatherResponse weather) => new()
    {
        Location = weather.Location.MapToLocation(),
        Temperature = Temperature.FromDegreesFahrenheit(weather.Current.TempC),
        FeelsLike = Temperature.FromDegreesFahrenheit(weather.Current.FeelslikeC),
        WindSpeed = Speed.FromMetersPerHour(weather.Current.WindMph),
        Pressure = Pressure.FromMegabars(weather.Current.PressureMb)
    };

    public static DailyWeather MapToDailyWeather(this DayDto dto) => new()
    {
        MaxTemperature = Temperature.FromDegreesCelsius(dto.MaxtempC),
        MinTemperature = Temperature.FromDegreesCelsius(dto.MintempC),
        AverageTemperature = Temperature.FromDegreesCelsius(dto.AvgtempC),
        MaxWindSpeed = Speed.FromKilometersPerHour(dto.MaxwindKph),
        TotalPrecipitation = Length.FromMillimeters(dto.TotalprecipMm),
        AverageVisibility = Length.FromKilometers(dto.AvgvisKm),
        AverageHumidity = RelativeHumidity.FromPercent(dto.Avghumidity),
        WillItRain = dto.DailyWillItRain == 1,
        ChanceOfRain = Ratio.FromPercent(dto.DailyChanceOfRain),
        WillItSnow = dto.DailyWillItSnow == 1,
        ChanceOfSnow = Ratio.FromPercent(dto.DailyChanceOfSnow),
        ConditionText = dto.Condition.Text,
        ConditionIcon = dto.Condition.Icon,
        ConditionCode = dto.Condition.Code,
        UvIndex = dto.Uv
    };

    public static HourlyWeather MapToHourlyWeather(this HourDto dto) => new()
    {
        Time = DateTimeOffset.FromUnixTimeSeconds(dto.TimeEpoch).DateTime,
        Temperature = Temperature.FromDegreesCelsius(dto.TempC),
        FeelsLike = Temperature.FromDegreesCelsius(dto.FeelslikeC),
        IsDay = dto.IsDay == 1,
        ConditionText = dto.Condition.Text,
        ConditionIcon = dto.Condition.Icon,
        ConditionCode = dto.Condition.Code,
        WindSpeed = Speed.FromKilometersPerHour(dto.WindKph),
        WindDirection = Angle.FromDegrees(dto.WindDegree),
        WindDirectionCompass = dto.WindDir,
        Pressure = Pressure.FromMillibars(dto.PressureMb),
        Precipitation = Length.FromMillimeters(dto.PrecipMm),
        Humidity = RelativeHumidity.FromPercent(dto.Humidity),
        CloudCover = Ratio.FromPercent(dto.Cloud),
        WindChill = Temperature.FromDegreesCelsius(dto.WindchillC),
        HeatIndex = Temperature.FromDegreesCelsius(dto.HeatindexC),
        DewPoint = Temperature.FromDegreesCelsius(dto.DewpointC),
        WillItRain = dto.WillItRain == 1,
        ChanceOfRain = Ratio.FromPercent(dto.ChanceOfRain),
        WillItSnow = dto.WillItSnow == 1,
        ChanceOfSnow = Ratio.FromPercent(dto.ChanceOfSnow),
        Visibility = Length.FromKilometers(dto.VisKm),
        GustSpeed = Speed.FromKilometersPerHour(dto.GustKph),
        UvIndex = dto.Uv
    };
}