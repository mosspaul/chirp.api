using System.Text.Json.Serialization;

namespace core.DTOs.SimpleFinDTOs;
public class Extra
    {
        [JsonPropertyName("account-open-date")]
        public int? AccountOpenDate { get; set; }
    }
