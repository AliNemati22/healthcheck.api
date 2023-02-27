using domain.Entities;
using domain.Interfaces;
using MediatR;

namespace healthcheck.api.Application.Features.Symbol.Queries;

public class GetSymbolesQuery : IRequest<IEnumerable<SymbolEntity>>
{

}

public class GetSymbolesQueryHandler : IRequestHandler<GetSymbolesQuery, IEnumerable<SymbolEntity>>
{

    private readonly IRepository<SymbolEntity> _symbolrepository;
    public GetSymbolesQueryHandler(IRepositoryAccessor repositoryAccessor, ILogger<GetOneSymbolesByIsinQueryHandler> logger)
    {
        _symbolrepository=repositoryAccessor.GetRepository<SymbolEntity>(reThrowException: true);
    }

    public async Task<IEnumerable<SymbolEntity>> Handle(GetSymbolesQuery request, CancellationToken cancellationToken)
    {
        var result = await _symbolrepository.GetAllAsync();
        return (List<SymbolEntity>)result.Data;
    }
}