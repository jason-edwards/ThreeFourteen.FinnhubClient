using Newtonsoft.Json;
using System;

namespace ThreeFourteen.Finnhub.Client.Model
{
    public class Company
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("cusip")]
        public string Cusip { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("ggroup")]
        public string Ggroup { get; set; }

        [JsonProperty("gind")]
        public string Gind { get; set; }

        [JsonProperty("gsector")]
        public string Gsector { get; set; }

        [JsonProperty("gsubind")]
        public string Gsubind { get; set; }

        [JsonProperty("isin")]
        public string Isin { get; set; }

        [JsonProperty("naics")]
        public string Naics { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        [JsonProperty("weburl")]
        public string Weburl { get; set; }

        [JsonProperty("ipo")]
        public DateTime Ipo { get; set; }

        [JsonProperty("sedol")]
        public string Sedol { get; set; }

        [JsonProperty("employeeTotal")]
        public string EmployeeTotal { get; set; }

        [JsonProperty("marketCapitalization")]
        public decimal MarketCap { get; set; }

        [JsonProperty("naicsNationalIndustry")]
        public string NaicsNatInd { get; set; }
        
        [JsonProperty("naicsSector")]
        public string NaicsSector { get; set; }

        [JsonProperty("naicsSubsector")]
        public string NaicsSubsector { get; set; }

        [JsonProperty("shareOutstanding")]
        public decimal ShareOutstanding { get; set; }
    }
}
