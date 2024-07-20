using System.Reflection;
using Arbitragem.Infrastructure.Exchanges.Implementations;
using Arbitragem.Infrastructure.Exchanges.Interfaces;
using ArbitraX.Application.Commands.SaveOrderBooks;
using ArbitraX.Application.Queries.GetOrderBooks;
using ArbitraX.Core.Repositories;
using ArbitraX.Infrastructure.Exchanges.Implementations;
using ArbitraX.Infrastructure.Exchanges.Interfaces;
using Solution.Infrastructure.Persistence.Context;
using ArbitraX.Infrastructure.Persistence.Repositories;
using ArbitraX.Worker;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;

var builder = Host.CreateApplicationBuilder(args);

var serverVersion = new MariaDbServerVersion(new Version(10, 4, 12));
var connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion),
    ServiceLifetime.Scoped);

//builder.Services.AddScoped(x =>
//  new MySqlConnection(connectionString));

builder.Services.AddScoped<IArbitrationRepository, ArbitrationRepository>();
builder.Services.AddScoped<IAwesomeService, AwesomeService>();
builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped(typeof(IDefaultRepository<>), typeof(DefaultRepository<>));
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddScoped<INetworkRepository, NetworkRepository>();
builder.Services.AddScoped<IOrderBookRepository, OrderBookRepository>();

builder.Services.AddTransient<IBinanceService, BinanceService>();
builder.Services.AddTransient<ICoinGeckoService, CoinGeckoService>();
builder.Services.AddTransient<IMercadoBitcoinService, MercadoBitcoinService>();

builder.Services.AddScoped<IRequestHandler<GetOrderBooksExchangesQuery, List<OrderBook>>, GetOrderBooksExchangesQueryHandler>();
builder.Services.AddTransient<IRequestHandler<SaveOrderBooksCommand, Unit>, SaveOrderBooksCommandHandle>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(typeof(Worker));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

