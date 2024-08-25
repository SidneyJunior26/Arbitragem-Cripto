using ArbitraX.Core.Repositories;
using MediatR;
using Solution.Core.Entities;

namespace Loader.Application.Commands.SaveDolar;

public class SaveDolarCommandHandle : IRequestHandler<SaveDolarCommand, Unit>
{
    private readonly IDefaultRepository<Dolar> _dolarRepository;

    public SaveDolarCommandHandle(IDefaultRepository<Dolar> dolarRepository)
    {
        _dolarRepository = dolarRepository;
    }

    public async Task<Unit> Handle(SaveDolarCommand request, CancellationToken cancellationToken)
    {
        var dolar = _dolarRepository.GetAllAsync().Result.FirstOrDefault();
        
        dolar.Update(request.Value);

        await _dolarRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}