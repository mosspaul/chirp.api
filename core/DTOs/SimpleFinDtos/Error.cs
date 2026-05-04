using Newtonsoft.Json;

public class Error
{
    [JsonProperty("code")]
    public required string Code { get; set; }

    [JsonProperty("msg")]
    public required string Message { get; set; }

    [JsonProperty("account_id")]
    public string? AccountId { get; set; }
    
    [JsonProperty("conn_id")]
    public string? ConnectionId { get; set; }
}