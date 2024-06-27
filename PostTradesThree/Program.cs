using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PostTradesThree.Data;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;
using PostTradesThree.Services;
using Serilog;
using System.Globalization;
using System.Text;

// Add Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/log-errors.txt",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
try
{
    Log.Information("Starting PostTradesThree Api");

    var builder = WebApplication.CreateBuilder(args);

    // Add configuration
    ConfigurationManager configuration = builder.Configuration;

    // Add culture
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[]
        {
        new CultureInfo("fr-FR"),
        new CultureInfo("fr")
    };

        options.DefaultRequestCulture = new RequestCulture(culture: "fr-FR", uiCulture: "fr-FR");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;

        options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
        {
            // My custom request culture logic
            return await Task.FromResult(new ProviderCultureResult("fr"));
        }));
    });

    // Add connection to the database
    var connectionString = configuration.GetConnectionString("PttConnectionString");
    builder.Services.AddDbContext<PttDbContext>(options =>
        options.UseSqlServer(connectionString));

    // Add connection to the user database
    var userConnectionString = configuration.GetConnectionString("PttUserConnectionString");
    builder.Services.AddDbContext<PttUserDbContext>(options =>
        options.UseSqlServer(userConnectionString));

    // Add dependency injection
    builder.Services.AddScoped<IBidRepository, BidRepository>();
    builder.Services.AddScoped<ICurvePointRepository, CurvePointRepository>();
    builder.Services.AddScoped<IRatingRepository, RatingRepository>();
    builder.Services.AddScoped<IRuleNameRepository, RuleNameRepository>();
    builder.Services.AddScoped<ITradeRepository, TradeRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();

    // Add Identity
    builder.Services
        .AddIdentity<PttUser, IdentityRole>()
        .AddEntityFrameworkStores<PttUserDbContext>()
        .AddDefaultTokenProviders();

    // Add password configuration
    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 12;
    });

    // Add authentication and JwtBearer configuration
    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = configuration["JWT:ValidIssuer"],
                ValidAudience = configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    // Add JWT Bearer authorize
    //builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "PostTradesThree_API",
            Version = "v1"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Add serilog http request
    //app.UseSerilogRequestLoggin();

    app.UseHttpsRedirection();

    // Add Authentication
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.Information("Exiting PostTradesThree Api");
    Log.CloseAndFlush();
}