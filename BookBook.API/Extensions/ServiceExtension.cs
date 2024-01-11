using BookBook.Repository;
using BookBook.Service;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination"));
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(opts =>
            {
                //We do not initialize any of the properties inside the options because we are fine with the default values for now
                //default
                // opts.AutomaticAuthentication = true;
                // opts.AuthenticationDisplayName = null;
                // opts.ForwardClientCertificate = true;
            });

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
            services.AddMySql<RepositoryContext>(connectionString,
                MySqlServerVersion.LatestSupportedServerVersion,
                mySqlOptions =>
                    mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    )

            );
            // services.AddDbContext<RepositoryContext>(options =>
            //     options.UseMySql(connectionString,
            //     MySqlServerVersion.LatestSupportedServerVersion,
            //     mySqlOptions =>
            //             mySqlOptions.EnableRetryOnFailure(
            //                 maxRetryCount: 10,
            //                 maxRetryDelay: TimeSpan.FromSeconds(30),
            //                 errorNumbersToAdd: null)));

        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        => services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services)
        => services.AddScoped<IServiceManager, ServiceManager>();

        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config =>
                config.OutputFormatters.Add(new CsvOutputFormatter()));
        }
        public static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }

    }
}