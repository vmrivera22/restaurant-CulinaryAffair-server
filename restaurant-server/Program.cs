using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using restaurant_server.Data;
using restaurant_server.Repositories;
using System.Security.Claims;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);

app.Run();


void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
    services.AddEndpointsApiExplorer();

    var keyVaultEndpoint = new Uri(configuration["VaultKey"]);
    var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());
    KeyVaultSecret authority = secretClient.GetSecret("authauth");
    KeyVaultSecret audience = secretClient.GetSecret("authaudience");

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.Authority = authority.Value;
        options.Audience = audience.Value;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });


    KeyVaultSecret kvs = secretClient.GetSecret("culinaryaffairsecret1");
    services.AddDbContext<DataContext>(o => o.UseSqlServer(kvs.Value));

    services.AddScoped<IDishesRepository, InMemDishesRepository>();
    services.AddScoped<IIngredientsRepository, InMemIngredientsRepository>();
    services.AddScoped<IOrdersRepository, InMemOrderRepository>();
    services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", builder =>
        {
            var corsOrigins = configuration.GetSection("CorsOrigins").Get<string[]>();
            builder.WithOrigins(corsOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
    services.AddSwaggerGen();
}

void Configure(IApplicationBuilder app, IHostEnvironment environment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseCors("AllowSpecificOrigin");

    app.UseAuthentication();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
