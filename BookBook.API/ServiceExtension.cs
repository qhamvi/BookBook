using BookBook.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookBook.API
{
    public static class ServiceExtension
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["mysqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
        }
        
    }
}