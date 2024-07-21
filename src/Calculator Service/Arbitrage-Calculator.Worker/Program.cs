using Arbitrage_Calculator;
using Arbitrage_Calculator.Application.Services.Implementations;
using Arbitrage_Calculator.Application.Services.Interfaces;
using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Implementations;
using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IOpportunityService, OpportunityService>();

builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<IOrderBookRepository, OrderBookRepository>();
builder.Services.AddScoped<IOpportunityRepository, OpportunityRepository>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

