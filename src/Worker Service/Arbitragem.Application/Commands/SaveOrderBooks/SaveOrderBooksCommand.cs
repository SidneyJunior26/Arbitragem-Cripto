using MediatR;
using Solution.Core.Entities;

namespace ArbitraX.Application.Commands.SaveOrderBooks;

public class SaveOrderBooksCommand : IRequest<Unit>
{
    public SaveOrderBooksCommand(List<OrderBook> orderBooks)
    {
        OrderBook = orderBooks;
    }

    public List<OrderBook> OrderBook { get; set; }
}

