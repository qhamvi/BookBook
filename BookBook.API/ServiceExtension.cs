using BookBook.Repository;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookBook.API
{
    public static class ServiceExtension
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["MySqlConnection:ConnectionString"];
            // services.AddDbContext<RepositoryContext>(o => 
            //     o.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion
            //     ));
            services.AddDbContext<RepositoryContext>(options =>
                options.UseMySql(connectionString,
                MySqlServerVersion.LatestSupportedServerVersion,
                mySqlOptions =>
                        mySqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)));

        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

    }
}