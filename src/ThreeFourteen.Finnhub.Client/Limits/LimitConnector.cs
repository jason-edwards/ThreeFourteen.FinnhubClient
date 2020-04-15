using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreeFourteen.Finnhub.Client.Limits
{
    public class LimitConnector
    {
        private ApiContext _dbContext;
        private long _weightMax;
        private int _weightPeriod; //in minutes
        internal LimitConnector(ApiContext apiContext)
        {
            _dbContext = apiContext;
            _weightMax = 60;
            _weightPeriod = 1;
        }

        public bool AddRequest(string request, long weight)
        {
            bool success = CheckWeightLimit(weight);
            if (success)
            {
                _dbContext.ApiRequests.Add(new ApiRequest { Description = request, Weight = weight, RequestTime = DateTime.Now });
                _dbContext.SaveChanges();
                
            }
            return success;
        }

        public bool CheckRateLimit()
        {

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
                    Thread.Sleep(500);
                    CheckWeightLimit(weightRequest);
                }
            }
            return true;
        }
    }
}
