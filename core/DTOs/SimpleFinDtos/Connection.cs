using Newtonsoft.Json;


namespace core.DTOs.SimpleFinDTOs;
public class Connection
    {
        [JsonProperty("conn_id")]
        public string ConnId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("org_id")]
        public string OrgId { get; set; }

        [JsonProperty("org_name")]
        public string OrgName { get; set; }

        [JsonProperty("org_url")]
        public string OrgUrl { get; set; }

        [JsonProperty("sfin_url")]
        public string SfinUrl { get; set; }
    }
