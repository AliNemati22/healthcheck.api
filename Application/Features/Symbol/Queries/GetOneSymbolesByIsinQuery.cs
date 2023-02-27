using domain.Entities;
using domain.Interfaces;
using MediatR;

namespace healthcheck.api.Application.Features.Symbol.Queries;

public class GetOneSymbolesByIsinQuery:IRequest<SymbolEntity>
{
    public string? Isin { get; set; }
}

public class GetOneSymbolesByIsinQueryHandler : IRequestHandler<GetOneSymbolesByIsinQuery, SymbolEntity>
{
    private readonly IRepository<SymbolEntity> _symbolrepository;
    public GetOneSymbolesByIsinQueryHandler(IRepositoryAccessor repositoryAccessor, ILogger<GetOneSymbolesByIsinQueryHandler> logger)
    {
        _symbolrepository=repositoryAccessor.GetRepository<SymbolEntity>(reThrowException: true);
    }

    public async Task<SymbolEntity> Handle(GetOneSymbolesByIsinQuery request, CancellationToken cancellationToken)
    {
        var result = await _symbolrepository.GetByIdAsync(request.Isin);
        return result.Data;
    }
}