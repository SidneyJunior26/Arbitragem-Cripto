using Arbitrage_Calculator;
using Arbitrage_Calculator.Application.Services.Implementations;
using Arbitrage_Calculator.Application.Services.Interfaces;
using Arbitrage_Calculator.Infrastructure.Email;
using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Implementations;
using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Solution.Infrastructure.Persistence.Context;

var builder = Host.CreateApplicationBuilder(args);

var serverVersion = new MariaDbServerVersion(new Version(10, 4, 12));
var connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion),
    ServiceLifetime.Scoped);

builder.Services.AddScoped(x =>
  new MySqlConnection(connectionString));

builder.Services.AddScoped<IOpportunityService, OpportunityService>();

builder.Services.AddScoped<IAdmRepository, AdmRepository>();
builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<IOrderBookRepository, OrderBookRepository>();
builder.Services.AddScoped<IOpportunityRepository, OpportunityRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

