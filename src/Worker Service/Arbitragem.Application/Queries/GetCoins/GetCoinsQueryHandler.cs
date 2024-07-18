using ArbitraX.Core.Repositories;
using MediatR;
using Solution.Core.Entities;

namespace ArbitraX.Application.Queries.GetCoins;

public class GetCoinsQueryHandler : IRequestHandler<GetCoinsQuery, List<Coin>>
{
    private readonly ICoinRepository _coinRepository;

    public GetCoinsQueryHandler(ICoinRepository coinRepository)
    {
        _coinRepository = coinRepository;
    }

    public Task<List<Coin>> Handle(GetCoinsQuery request, CancellationToken cancellationToken)
    {
        var result = _coinRepository.GetAll();

        throw new NotImplementedException();
    }
}

