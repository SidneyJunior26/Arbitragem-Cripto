using System.Reflection;
using ArbitraX.Application.Commands.SaveOrderBooks;
using ArbitraX.Application.Queries.GetOrderBooks;
using ArbitraX.Core.Repositories;
using Solution.Infrastructure.Persistence.Context;
using ArbitraX.Infrastructure.Persistence.Repositories;
using ArbitraX.Worker;
using Loader.Application.Commands.SaveDolar;
using Loader.Infrastructure.Kafka.Implementation;
using Loader.Infrastructure.Kafka.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Solution.Core.Entities;
using ArbitraX.Infrastructure.Services.Interfaces;
using ArbitraX.Infrastructure.Services;
using Arbitragem.Infrastructure.Services;
using Loader.Core.Interfaces;
using Loader.Infrastructure.Services;

var builder = Host.CreateApplicationBuilder(args);

var serverVersion = new MariaDbServerVersion(new Version(10, 4, 12));
var connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion),
    ServiceLifetime.Transient);

builder.Services.AddTransient(x =>
  new MySqlConnection(connectionString));

builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped(typeof(IDefaultRepository<>), typeof(DefaultRepository<>));
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddScoped<INetworkRepository, NetworkRepository>();
builder.Services.AddScoped<IOrderBookRepository, OrderBookRepository>();
builder.Services.AddScoped<IAdmConfigRepository, AdmConfigRepository>();

builder.Services.AddScoped<IAwesomeService, AwesomeService>();

builder.Services.AddScoped<IExchangeService, BinanceService>();
builder.Services.AddScoped<IExchangeService, BitfinexService>();
builder.Services.AddScoped<IExchangeService, BitgetService>();
builder.Services.AddScoped<IExchangeService, BrasilBitcoinService>();
builder.Services.AddScoped<IExchangeService, BybitService>();
builder.Services.AddScoped<IExchangeService, ChilizService>();
builder.Services.AddScoped<IExchangeService, KucoinService>();
builder.Services.AddScoped<IExchangeService, MercadoBitcoinService>();
builder.Services.AddScoped<IExchangeService, NovaDaxService>();
builder.Services.AddScoped<IExchangeService, OkxService>();
builder.Services.AddScoped<IExchangeService, WhitebitService>();

builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();

builder.Services.AddScoped<IRequestHandler<GetOrderBooksExchangesQuery, List<OrderBook>>, GetOrderBooksExchangesQueryHandler>();
builder.Services.AddScoped<IRequestHandler<SaveOrderBooksCommand, Unit>, SaveOrderBooksCommandHandle>();
builder.Services.AddScoped<IRequestHandler<SaveDolarCommand, Unit>, SaveDolarCommandHandle>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(typeof(Worker));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

