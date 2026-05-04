using Newtonsoft.Json;

namespace core.DTOs.SimpleFinDTOs;
public class AccountSet
    {
        [JsonProperty("errlist")]
        public List<Error> Errlist { get; set; }

        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; }

        [JsonProperty("connections")]
        public List<Connection> Connections { get; set; }
    }
