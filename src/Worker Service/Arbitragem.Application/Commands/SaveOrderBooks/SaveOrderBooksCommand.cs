using MediatR;
using Solution.Core.Entities;

namespace ArbitraX.Application.Commands.SaveOrderBooks;

public class SaveOrderBooksCommand : IRequest<Unit>
{
    public SaveOrderBooksCommand(List<OrderBook> orderBooks, string symbol)
    {
        OrderBook = orderBooks;
        Symbol = symbol;
    }

    public List<OrderBook> OrderBook { get; set; }
    public string Symbol { get; set; }
}

