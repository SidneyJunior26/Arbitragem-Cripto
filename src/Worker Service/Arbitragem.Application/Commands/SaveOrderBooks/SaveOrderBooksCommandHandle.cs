﻿using ArbitraX.Core.Repositories;
using MediatR;

namespace ArbitraX.Application.Commands.SaveOrderBooks;

public class SaveOrderBooksCommandHandle : IRequestHandler<SaveOrderBooksCommand, Unit>
{
    private readonly IOrderBookRepository _orderBookRepository;

    public SaveOrderBooksCommandHandle(IOrderBookRepository orderBookRepository)
    {
        _orderBookRepository = orderBookRepository;
    }

    public async Task<Unit> Handle(SaveOrderBooksCommand request, CancellationToken cancellationToken)
    {
        if (request.OrderBook.Any())
        {
            var olderOrderBook = await _orderBookRepository
                .GetListBySymbolsAndExchangeAndSide(request.OrderBook[0].Coin.Symbol, request.OrderBook[0].ExchangeId, request.OrderBook[0].Side);

            if (olderOrderBook != null)
                _orderBookRepository.DeleteOrdersBooksList(olderOrderBook);

            await _orderBookRepository.CreateOrderBooksListAsync(request.OrderBook);
        }

        await _orderBookRepository.SaveChangesAsync();

        return Unit.Value;
    }
}

