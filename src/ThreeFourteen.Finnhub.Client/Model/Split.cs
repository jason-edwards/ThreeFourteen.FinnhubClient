using Newtonsoft.Json;
using System;

namespace ThreeFourteen.Finnhub.Client.Model
{
    public class Split
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("fromFactor")]
        public decimal fromFactor { get; set; }

        [JsonProperty("toFactor")]
        public decimal toFactor { get; set; }
    }
}
