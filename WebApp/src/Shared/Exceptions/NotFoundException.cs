namespace WebApp.Shared.Exceptions;

public class NotFoundException(string message, int code = 500): Exception(message)
{
    public readonly int Code = code;
}