using BookBook.Repository;
using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;

namespace BookBook.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            
        }
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
                            maxRetryDelay: TimeSpan.FromSeconds(100),
                            errorNumbersToAdd: null)));

        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

    }
}