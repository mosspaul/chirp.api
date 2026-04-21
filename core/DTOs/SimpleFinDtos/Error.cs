using System.Text.Json.Serialization;

public class Error
{
    [JsonPropertyName("code")]
    public required string Code { get; set; }

    [JsonPropertyName("msg")]
    public required string Message { get; set; }

    [JsonPropertyName("account_id")]
    public string? AccountId { get; set; }
    
    [JsonPropertyName("conn_id")]
    public string? ConnectionId { get; set; }
}