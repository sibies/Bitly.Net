namespace Bitly.Net.v4.Responses;

public class BitlyGeneralErrorResponse : BitlyBaseResponse
{
    public BitlyGeneralErrorResponse(string message)
    {
        Message = message;
    }

    public override bool Success => false;
    public override string Message { get; }
}