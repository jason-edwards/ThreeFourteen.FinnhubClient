using Serilog;
using System;
using System.Linq;
using System.Threading;

namespace ThreeFourteen.Finnhub.Client.Limits
{
    public class LimitConnector
    {
        private ApiContext _dbContext;
        private long _weightMax;
        private int _weightPeriod; //in minutes
        private int _rateMax; //calls per second.
        internal LimitConnector(ApiContext apiContext)
        {
            _dbContext = apiContext;
            _weightMax = 60;
            _rateMax = 30; 
            _weightPeriod = 1;
        }

        public bool AddRequest(string request, long weight)
        {
            bool success = CheckWeightLimit(weight);
            success = success && CheckRateLimit();
            if (success)
            {
                _dbContext.ApiRequests.Add(new ApiRequest { Description = request, Weight = weight, RequestTime = DateTime.Now });
                _dbContext.SaveChanges();
                
            }
            return success;
        }

        public bool CheckRateLimit()
        {
            var records = _dbContext.ApiRequests
                .Where(b => b.RequestTime > DateTime.Now.AddSeconds(-1));

            if(records.Count() > _rateMax)
            {
                Thread.Sleep(100);
                CheckRateLimit();
            }
            return true;
        }

        public bool CheckWeightLimit(long weightRequest)
        {
            if (weightRequest > _weightMax)
            {
                Log.Information($"Api Request failed. " +
                    $"Request type has a request weight that is too large. {weightRequest} > {_weightMax}");
                return false;
            }
            else
            {
                var records = _dbContext.ApiRequests
                    .Where(b => b.RequestTime > DateTime.Now.AddMinutes(-_weightPeriod));

                long weightTotal = weightRequest;
                foreach (var record in records)
                {
                    weightTotal += record.Weight;
                }

                if (weightTotal > _weightMax)
                {
                    Thread.Sleep(100);
                    CheckWeightLimit(weightRequest);
                }
            }
            return true;
        }
    }
}
