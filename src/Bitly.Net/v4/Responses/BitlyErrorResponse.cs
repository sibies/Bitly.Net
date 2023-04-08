namespace Bitly.Net.v4.Responses;

public class BitlyErrorResponse : BitlyBaseResponse
{
    public string message { get; set; }
    public string resource { get; set; }
    public string description { get; set; }
    public BitlyResponseError[] errors { get; set; }

    public override bool Success => false;
    public override string Message => message;
}