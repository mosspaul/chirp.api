using Newtonsoft.Json;

namespace core.DTOs.SimpleFinDTOs;

public class SfinAccountDto
{
    [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("available-balance")]
        public string AvailableBalance { get; set; }

        [JsonProperty("balance-date")]
        public int BalanceDate { get; set; }

        [JsonProperty("transactions")]
        public List<SfinTransactionDto> Transactions { get; set; }

        [JsonProperty("holdings")]
        public List<SfinHoldingDto> Holdings { get; set; }

        [JsonProperty("conn_id")]
        public string ConnId { get; set; }
}
