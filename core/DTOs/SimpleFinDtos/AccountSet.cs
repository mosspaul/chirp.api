using System.Text.Json.Serialization;

namespace core.DTOs.SimpleFinDTOs;
public class AccountSet
    {
        [JsonPropertyName("errlist")]
        public required List<Error> Errors { get; set; }

        [JsonPropertyName("connections")]
        public required List<Connection> Connections { get; set; }

        [JsonPropertyName("accounts")]
        public required List<Account> Accounts { get; set; }
    }
