using MediatR;
using Solution.Core.Entities;

namespace ArbitraX.Application.Queries.GetOrderBooks;

public class GetOrderBooksQuery : IRequest<List<OrderBook>>
{
    public GetOrderBooksQuery(int limit)
    {
        Limit = limit;
    }

    public int Limit { get; set; }
}