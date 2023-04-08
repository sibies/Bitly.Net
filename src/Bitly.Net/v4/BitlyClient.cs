using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Bitly.Net.v4.Requests;
using Bitly.Net.v4.Responses;

namespace Bitly.Net.v4
{
    public class BitlyClient : IBitlyClient, IDisposable
    {
        private const string ApiUrl = "https://api-ssl.bitly.com/v4/shorten";
        private const string JsonMediaType = "application/json";
        readonly string _token;
        readonly HttpClient _client;

        public BitlyClient(string token)
        {
            _token = token;
            _client = new HttpClient();
        }

        public async Task<BitlyBaseResponse> ShortenAsync(string urlToShorten)
        {
            var jsonString = JsonSerializer.Serialize(new BitlyRequest { LongUrl = urlToShorten });

            var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl)
            {
                Content = new StringContent(jsonString, Encoding.UTF8, JsonMediaType)
            };

            try
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await _client.SendAsync(request).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    return new BitlyGeneralErrorResponse(response.StatusCode.ToString());
                var stringResponse = await response.Content.ReadAsStringAsync();

                if (stringResponse.Contains("errors"))
                {
                    var jsonErrorResponse = JsonSerializer.Deserialize<BitlyErrorResponse>(stringResponse);
                    return jsonErrorResponse;
                }
                var jsonResponse = JsonSerializer.Deserialize<BitlySuccessResponse>(stringResponse);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                return new BitlyGeneralErrorResponse(ex.Message);
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
