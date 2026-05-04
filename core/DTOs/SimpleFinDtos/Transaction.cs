using Newtonsoft.Json;

namespace core.DTOs.SimpleFinDTOs;

public class Transaction
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("posted")]
    public int Posted { get; set; }

    [JsonProperty("amount")]
    public string Amount { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("payee")]
    public string Payee { get; set; }

    [JsonProperty("memo")]
    public string Memo { get; set; }

    [JsonProperty("transacted_at")]
    public int TransactedAt { get; set; }

    public DateTime ConvertTimestampToDate(int unixTimestamp)
    {
        var date = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
        return date;
    }
}
