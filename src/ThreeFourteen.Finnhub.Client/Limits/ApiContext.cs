using Microsoft.EntityFrameworkCore;

namespace ThreeFourteen.Finnhub.Client.Limits
{
    public class ApiContext : DbContext
    {
        public DbSet<ApiRequest> ApiRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=ApiLimits.db");
    }
}
