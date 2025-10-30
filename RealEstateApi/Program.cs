using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Identity.Web;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Caching.Memory;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RealEstate.Api;
using RealEstate.Core;
using RealEstate.Domain.Repositories;
using System.Text;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using RealEstate.Api.Infrastructure.Swagger;
using RealEstate.Api.Infrastructure.Integrations.OpenApi;
using RealEstate.Core.Cache;

const string serviceName = "RealEstate.Api";
var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddOpenTelemetry(options =>
{
    options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName)).AddConsoleExporter();
});

var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resourceBuilder => resourceBuilder.AddService(serviceName))
    .WithTracing(traces => traces
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter())
    .WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter());

if (string.Equals(configuration.GetValue<string>("EnvironmentName"), "PRD", StringComparison.OrdinalIgnoreCase)
    || string.Equals(configuration.GetValue<string>("EnvironmentName"), "DEV", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddOpenTelemetry().UseAzureMonitor(options =>
    {
        options.SamplingRatio = 0.05F;
    });
}

// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "CustomJWT",
        Reference = new OpenApiReference
        {
            Id = "CustomJWT",
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>() }
    });
    setup.OperationFilter<HeaderOperationFilter>();
    setup.SchemaFilter<RequiredSchemaFilter>();
    setup.SupportNonNullableReferenceTypes();
    setup.UseAllOfToExtendReferenceSchemas();
});

builder.Services.AddMvc();

// Configure authentication with both JWT Bearer and Microsoft Identity Web API
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "CustomJWT";
    options.DefaultChallengeScheme = "CustomJWT";
})
    .AddJwtBearer("CustomJWT", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    })
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("Authentication"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddDownstreamApi("ExchangeRateApi", builder.Configuration.GetSection("ExchangeRateApi"))
    .AddInMemoryTokenCaches();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority = "https://dev-sc4pub2ho26ipnxv.us.auth0.com/";
//    options.Audience = "https://localhost:7064/";
//});

builder.Services.AddResponseCaching();
builder.Services.AddDbContext<RealEstate.Domain.DBI.RealEstateDbContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=RealEstate", builder => builder.MigrationsAssembly("RealEstate.Api"))
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IRequestContextProvider, RequestContextProvider>();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();
app.MapControllers();

app.Run();
