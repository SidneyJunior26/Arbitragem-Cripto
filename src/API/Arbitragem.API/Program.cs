using System.Reflection;
using System.Text;
using Arbitragem.Application.Services.Implementations;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Infrastructure.Persistence.Repositories.Implementations;
using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using ArbitraX.Core.Repositories;
using ArbitraX.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Solution.Infrastructure.Persistence.Context;
using DolarRepository = Arbitragem.Infrastructure.Persistence.Repositories.Implementations.DolarRepository;
using ExchangeRepository = Arbitragem.Infrastructure.Persistence.Repositories.Implementations.ExchangeRepository;
using IDolarRepository = Arbitragem.Infrastructure.Persistence.Repositories.Interfaces.IDolarRepository;
using IExchangeRepository = Arbitragem.Infrastructure.Persistence.Repositories.Interfaces.IExchangeRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var serverVersion = new MariaDbServerVersion(new Version(10, 4, 12));
var connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(connectionString, serverVersion));

builder.Services.AddTransient(x =>
    new MySqlConnection(connectionString));

builder.Services.AddScoped<IAdmConfigurationService, AdmConfigurationService>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<IExchangeService, ExchangeService>();
builder.Services.AddScoped<IOrderBookService, OrderBookService>();
builder.Services.AddScoped<IOpportunityService, OpportunityService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IDolarService, DolarService>();

builder.Services.AddScoped<IAdmConfigurationRepository, AdmConfigurationRepository>();
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddScoped<IOrderBookRepository, OrderBookRepository>();
builder.Services.AddScoped<IOpportunityRepository, OpportunityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IDolarRepository, DolarRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArbitraX API", Version = "v1" });
    
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme {
            Description = @"Cabeçalho de autorização JWT usando o esquema Bearer. \r\n\r\n 
                      Digite 'Bearer' [espaço] e, em seguida, seu token na entrada de texto abaixo.
                      Exemplo: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy("AdmPolicy", p =>
        p.RequireAuthenticatedUser().RequireClaim("adm"));
    options.AddPolicy("UserPolicy", p =>
        p.RequireAuthenticatedUser().RequireClaim("email"));
});
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArbitraX V1");
    });
}

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext http) => {
    var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error;

    if (error != null) {
        switch (error) {
            case MySqlException:
                return Results.Problem(title: "Databse out", statusCode: 500);
            case FormatException:
                return Results.Problem(title: "Error to convert data to other type. Confirm all information sent", statusCode: 500);
        }
    }

    return Results.Problem(title: "An error ocurred", statusCode: 500);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();

