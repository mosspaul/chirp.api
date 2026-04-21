using System.Text.Json.Serialization;


namespace core.DTOs.SimpleFinDTOs;
public class Connection
    {
        [JsonPropertyName("conn_id")]
        public required string ConnectionId { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("org_id")]
        public required string OrganizationId { get; set; }

        [JsonPropertyName("org_url")]
        public string? OrganizationUrl { get; set; }

        [JsonPropertyName("sfin_url")]
        public required string SimpleFinUrl { get; set; }
    }
