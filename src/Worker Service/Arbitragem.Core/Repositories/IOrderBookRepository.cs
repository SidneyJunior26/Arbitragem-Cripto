﻿using Solution.Core.Entities;
using Solution.Core.Enum;
using Solution.Infrastructure.Persistence.Context;

namespace ArbitraX.Core.Repositories;

public interface IOrderBookRepository
{
    Task CreateOrderBooksListAsync(List<OrderBook> orderBooks);
    Task CreateOrderBookAsync(OrderBook orderBook);
    Task<List<OrderBook>> GetAll(string coins);
    Task<List<OrderBook>> GetListBySymbolsAndExchangeAndSide(string symbol, Guid exchangeId);
    void UpdateOrderBooksAsync(List<OrderBook> orderBooks);
    void DeleteOrdersBooksList(List<OrderBook> orderBooks);
    Task DeleteOrderOrderBooksAsync();
    Task SaveChangesAsync();
}

