using System.Text.Json.Serialization;

namespace Bitly.Net.v4.Requests;

public class BitlyRequest
{
    [JsonPropertyName("long_url")]
    public string LongUrl { get; set; }
}