using MediatR;
using Solution.Core.Entities;

namespace ArbitraX.Application.Queries.GetOrderBooks;

public class GetOrderBooksExchangesQuery : IRequest<List<OrderBook>>
{
    public GetOrderBooksExchangesQuery(int limit)
    {
        Limit = limit;
    }

    public int Limit { get; set; }
}