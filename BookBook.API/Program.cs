using AspNetCoreRateLimit;
using BookBook.API;
using BookBook.API.Extensions;
using BookBook.DTOs.DataTransferObject;
using BookBook.DTOs.MappingProfile;
using BookBook.Presentation;
using BookBook.Service;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Shared;
using static BookBook.API.Extensions.ServiceExtension;

// NewtonsoftJsonInputFormatter GetJsonPatchInputFormatter() => 
//     new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson().Services.BuildServiceProvider()
//     .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
//     .OfType<NewtonsoftJsonPatchInputFormatter>()
//     .First();

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureMySqlContext(builder.Configuration);
builder.Services.ConfigureRepository();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigurettpCacheHeader();
builder.Services.AddMemoryCache();
builder.Services.ConfigurationRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ConfigureSwagger();

builder.Services.AddAutoMapper(typeof(AuthorMappingProfile), typeof(BookMappingProfile));
//Enable custom responses from the actions
builder.Services.Configure<ApiBehaviorOptions>(opts => 
{
    opts.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(config => {
    
    config.Filters.Add<GlobalActionFilterExample>();
    // config.Filters.Add(new GlobalActionFilterExample());
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter()); // only 
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile {
        Duration = 120
        //more 
    });
}).AddXmlDataContractSerializerFormatters()
  .AddCustomCsvFormatter()
//   .AddNewtonsoftJson() => replacing System.Text.Json formatters for all JSON content
  .AddApplicationPart(typeof(BookBook.Presentation.AssemblyReference).Assembly);

builder.Services.AddScoped<ActionFilterExample>();
builder.Services.AddScoped<ControllerFilterExample>();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<IDataShapper<AuthorDto>, DataShapper<AuthorDto>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(config =>
// {
//     config.EnableAnnotations();
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
app.UseMiddleware<GlobalRoutePrefixMiddleware>("/api");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwaggerUI(opt =>
    {
       opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Book API"); 
       opt.SwaggerEndpoint("/swagger/v2/swagger.json", "Book Book API 2"); 
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions 
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseIpRateLimiting();
app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseHttpCacheHeaders();
app.UseAuthentication();
//app.MapControllers();
app.UsePathBase(new PathString("/api"));
app.UseRouting();
//When authorizing a resource that is routed using endpoint routing, this call must appear between the calls to app.UseRouting() and app.UseEndpoints(...) for the middleware to function correctly.
app.UseAuthorization();
app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        // Other endpoints...
    }); // === app.MapControllers();
app.Run();
