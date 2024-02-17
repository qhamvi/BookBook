using System.Text;
using AspNetCoreRateLimit;
using BookBook.Models;
using BookBook.Models.ConfigurationModels;
using BookBook.Repository;
using BookBook.Service;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BookBook.API.Extensions
{
    public static class ServiceExtension
    {
        /// <summary>
        /// Configure CORS
        /// </summary>
        /// <param name="services"></param>
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
        /// <summary>
        /// Adding Json Patch Input
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Adding Cache-Store
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
        }
        /// <summary>
        /// Add HttpCacheHeader
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigurettpCacheHeader(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders((expiretionOpt) =>
            {
                expiretionOpt.MaxAge = 60;
                expiretionOpt.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
            },
            (validationOpt) =>
            {
                validationOpt.MustRevalidate = true;
            }
            );
        }
        public static void ConfigurationRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 100,
                    Period = "10m"
                }
            };
            services.Configure<IpRateLimitOptions>(opts =>
            {
                opts.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequiredLength = 8;
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = new JwtConfiguration();
            configuration.Bind(jwtSetting.Section, jwtSetting);
            configuration.Bind("JwtSettingsV2", jwtSetting);
            //    var jwtSetting = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSetting.ValidIssuer,
                    ValidAudience = jwtSetting.ValidAudience,
                    // ValidIssuer = jwtSetting["ValidIssuer"],
                    // ValidAudience = jwtSetting["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));
            services.Configure<JwtConfiguration>("JwtSettings", configuration.GetSection("JwtSettings"));
            services.Configure<JwtConfiguration>("JwtSettingsV2", configuration.GetSection("JwtSettingsV2"));
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Book Book API", 
                    Version = "v1" ,
                    Description = " Book API by qhamvi",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Vi Pham",
                        Email = "qhamvi@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/qhamvi/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "ViVi test",
                        Url = new Uri("https://example.com/license")
                    }});
              
                var xmlFile = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile) ;
                config.IncludeXmlComments(xmlPath);

                config.SwaggerDoc("v2", new OpenApiInfo { Title = "Book Book API 2", Version = "v2" });
                config.EnableAnnotations();

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });

            });
        }

    }
}