namespace Bitly.Net.v4.Responses;

public abstract class BitlyBaseResponse
{
    public abstract bool Success { get; }
    public abstract string Message { get; }
}