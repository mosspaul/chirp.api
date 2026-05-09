using Newtonsoft.Json;

namespace core.DTOs.SimpleFinDTOs;
public class AccountSetDto
    {
        [JsonProperty("errlist")]
        public List<ErrorDto> Errlist { get; set; }

        [JsonProperty("accounts")]
        public List<AccountDto> Accounts { get; set; }

        [JsonProperty("connections")]
        public List<ConnectionDto> Connections { get; set; }
    }
