using domain.Entities;
using domain.Interfaces;
using MediatR;

namespace healthcheck.api.Application.Features.Symbol.Queries;

public class GetActiveSymbolesQuery : IRequest<IEnumerable<SymbolEntity>>
{
}


public class GetActiveSymbolesQueryHandler : IRequestHandler<GetActiveSymbolesQuery, IEnumerable<SymbolEntity>>
{

    private readonly IRepository<SymbolEntity> _symbolrepository;
    private List<SymbolEntity> _cache;
    private DateTime _lastFetch;
    public GetActiveSymbolesQueryHandler(IRepositoryAccessor repositoryAccessor, ILogger<GetOneSymbolesByIsinQueryHandler> logger)
    {
        _symbolrepository=repositoryAccessor.GetRepository<SymbolEntity>(reThrowException: true);
    }
    public async Task<IEnumerable<SymbolEntity>> Handle(GetActiveSymbolesQuery request, CancellationToken cancellationToken)
    {
        if (DateTime.Now - _lastFetch> TimeSpan.FromHours(6))
        {
            var symbols = (await _symbolrepository.QueryAsync(p => p.IsActive && p.IsAuthorized)).Data;
            _cache = (List<SymbolEntity>)symbols;
            _lastFetch = DateTime.Now;
        }
        return _cache;
    }
}