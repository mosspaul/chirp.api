using System.Text.Json.Serialization;

namespace core.DTOs.SimpleFinDTOs;

public class Transaction
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("posted")]
    public required int Posted { get; set; }

    [JsonPropertyName("amount")]
    public required string Amount { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("transacted_at")]
    public int? TransactionDate { get; set; }

    [JsonPropertyName("pending")]
    public bool? Pending { get; set; }

    public DateTime ConvertTimestampToDate(int unixTimestamp)
    {
        var date = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
        return date;
    }
}
