namespace WeatherClient.Abstractions.Exceptions;

public class UnableParseResultException : Exception
{
    public UnableParseResultException()
    {
    }

    public UnableParseResultException(string? message) : base(message)
    {
    }

    public UnableParseResultException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}