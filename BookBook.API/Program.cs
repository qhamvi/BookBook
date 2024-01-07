using BookBook.API.Extensions;
using BookBook.DTOs.MappingProfile;
using BookBook.Presentation;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
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
}).AddXmlDataContractSerializerFormatters()
  .AddCustomCsvFormatter()
//   .AddNewtonsoftJson() => replacing System.Text.Json formatters for all JSON content
  .AddApplicationPart(typeof(BookBook.Presentation.AssemblyReference).Assembly);

builder.Services.AddScoped<ActionFilterExample>();
builder.Services.AddScoped<ControllerFilterExample>();
builder.Services.AddScoped<ValidationFilterAttribute>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
