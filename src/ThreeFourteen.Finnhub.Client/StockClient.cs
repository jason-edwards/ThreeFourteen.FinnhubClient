using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeFourteen.Finnhub.Client.Limits;
using ThreeFourteen.Finnhub.Client.Model;
using ThreeFourteen.Finnhub.Client.Serialisation;

namespace ThreeFourteen.Finnhub.Client
{
    public class StockClient
    {
        private readonly FinnhubClient _finnhubClient;
        private readonly LimitConnector _limitConnector;

        internal StockClient(FinnhubClient finnhubClient)
        {
            ApiContext apiContext = new ApiContext();
            
            _finnhubClient = finnhubClient;
            _limitConnector = new LimitConnector(apiContext);
        }

        public Task<Company> GetCompany(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetRecommendationTrends", 20);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<Company>("stock/profile", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        /*public Task<Compensation> GetCompensation(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetRecommendationTrends", 1);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<Compensation>("stock/ceo-compensation", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }*/

        public Task<RecommendationTrend[]> GetRecommendationTrends(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetRecommendationTrends", 1);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<RecommendationTrend[]>("stock/recommendation", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<PriceTarget> GetPriceTarget(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetPriceTarget", 1);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<PriceTarget>("stock/price-target", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<string[]> GetPeers(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetEarnings", 1);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<string[]>("stock/peers", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<Earnings[]> GetEarnings(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetEarnings", 10);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<Earnings[]>("stock/earnings", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<StockExchange[]> GetExchanges()
        {
            bool limitCheck = _limitConnector.AddRequest("GetExchanges", 1);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<StockExchange[]>("stock/exchange", JsonDeserialiser.Default);
        }

        public Task<Symbol[]> GetSymbols(string exchange)
        {
            bool limitCheck = _limitConnector.AddRequest("GetSymbols", 1);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<Symbol[]>("stock/symbol", JsonDeserialiser.Default,
                new Field(FieldKeys.Exchange, exchange));
        }

        public Task<Quote> GetQuote(string symbol)
        {
            bool limitCheck = _limitConnector.AddRequest("GetQuote", 1);
            if (!limitCheck)
            {
                return null;
            }

            return _finnhubClient.SendAsync<Quote>("quote", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public async Task<Candle[]> GetCandles(string symbol, Resolution resolution, int count)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetCandles", 1);
            if (!limitCheck)
            {
                return null;
            }

            var data = await _finnhubClient.SendAsync<CandleData>("stock/candle", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.Resolution, resolution.GetFieldValue()),
                new Field(FieldKeys.Count, count.ToString()))
                .ConfigureAwait(false);

            return data.Map();
        }

        public Task<Candle[]> GetCandles(string symbol, Resolution resolution, DateTime from)
        {
            return GetCandles(symbol, resolution, from, DateTime.UtcNow);
        }

        public async Task<Candle[]> GetCandles(string symbol, Resolution resolution, DateTime from, DateTime to, bool adjusted=false)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));
            
            bool limitCheck = _limitConnector.AddRequest("GetCandles", 1);
            if (!limitCheck)
            {
                return null;
            }

            var data = await _finnhubClient.SendAsync<CandleData>("stock/candle", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.Resolution, resolution.GetFieldValue()),
                new Field(FieldKeys.From, new DateTimeOffset(from).ToUnixTimeSeconds().ToString()),
                new Field(FieldKeys.To, new DateTimeOffset(to).ToUnixTimeSeconds().ToString()),
                new Field(FieldKeys.Adjusted, adjusted.ToString().ToLower()))
                .ConfigureAwait(false);

            return data.Map();
        }

        public async Task<Dividend[]> GetDividends(string symbol, DateTime from, DateTime to)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetDividends", 1);
            if (!limitCheck)
            {
                return null;
            }

            var data = await _finnhubClient.SendAsync<Dividend[]>("stock/dividend", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.From, from.ToString("yyyy-M-dd")),
                new Field(FieldKeys.To, to.ToString("yyyy-M-dd")))
                .ConfigureAwait(false);

            return data;
        }

        public async Task<Split[]> GetSplits(string symbol, DateTime from, DateTime to)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            bool limitCheck = _limitConnector.AddRequest("GetSplits", 1);
            if (!limitCheck)
            {
                return null;
            }
            
            var data = await _finnhubClient.SendAsync<Split[]>("stock/split", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.From, from.ToString("yyyy-M-dd")),
                new Field(FieldKeys.To, to.ToString("yyyy-M-dd")))
                .ConfigureAwait(false);

            return data;



        }
    }
}