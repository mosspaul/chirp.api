using System.Text.Json.Serialization;

namespace core.DTOs.SimpleFinDTOs;

public class Account
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("conn_id")]
    public required string ConnectionId { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("balance")]
    public required string Balance { get; set; }

    [JsonPropertyName("available-balance")]
    public string? AvailableBalance { get; set; }

    [JsonPropertyName("balance-date")]
    public int BalanceDate { get; set; }

    [JsonPropertyName("transactions")]
    public List<Transaction>? Transactions { get; set; }

    [JsonPropertyName("extra")]
    public Extra? Extra { get; set; }

    public DateTime ConvertTimestampToDate(int unixTimestamp)
    {
        var date = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
        return date;
    }
}
