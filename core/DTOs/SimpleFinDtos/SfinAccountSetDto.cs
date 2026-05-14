using Newtonsoft.Json;

namespace core.DTOs.SimpleFinDTOs;
public class SfinAccountSetDto
    {
        [JsonProperty("errlist")]
        public List<SfinErrorDto> Errlist { get; set; }

        [JsonProperty("accounts")]
        public List<SfinAccountDto> Accounts { get; set; }

        [JsonProperty("connections")]
        public List<SfinConnectionDto> Connections { get; set; }
    }
