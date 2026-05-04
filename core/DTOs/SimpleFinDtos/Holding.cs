using Newtonsoft.Json;

namespace core.DTOs.SimpleFinDTOs;
public class Holding
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("cost_basis")]
        public string CostBasis { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("market_value")]
        public string MarketValue { get; set; }

        [JsonProperty("purchase_price")]
        public string PurchasePrice { get; set; }

        [JsonProperty("shares")]
        public string Shares { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
