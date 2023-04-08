using Bitly.Net.v4.Responses;

namespace Bitly.Net.v4;

public interface IBitlyClient
{
    Task<BitlyBaseResponse> ShortenAsync(string urlToShorten);
}