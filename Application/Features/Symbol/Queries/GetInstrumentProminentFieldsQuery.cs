using domain.Entities;
using domain.Interfaces;
using MediatR;
using Raven.Client.Documents.Linq;

namespace healthcheck.api.Application.Features.Symbol.Queries;

public class GetInstrumentProminentFieldsQuery : IRequest<IEnumerable<InstrumentProminentFieldsEntity>>
{
}


public class GetInstrumentProminentFieldsQueryHandler : IRequestHandler<GetInstrumentProminentFieldsQuery, IEnumerable<InstrumentProminentFieldsEntity>>
{
    private readonly IRepository<InstrumentProminentFieldsEntity> _instrumentProminentFieldsRepository;
    private readonly IMediator _mediator;
    private List<InstrumentProminentFieldsEntity> _cache;
    private DateTime _lastFetch;

    public GetInstrumentProminentFieldsQueryHandler(IRepositoryAccessor repositoryAccessor, IMediator mediator)
    {
        _instrumentProminentFieldsRepository=repositoryAccessor.GetRepository<InstrumentProminentFieldsEntity>(reThrowException: true);
        _mediator=mediator;
    }

    public async Task<IEnumerable<InstrumentProminentFieldsEntity>> Handle(GetInstrumentProminentFieldsQuery request, CancellationToken cancellationToken)
    {
        var activeSymbols = await _mediator.Send(new GetActiveSymbolesQuery());
        var isins = activeSymbols.Select(s => s.SymbolIsin).ToList();

        if (DateTime.Now - _lastFetch> TimeSpan.FromHours(6))
        {
            var instrumentProminentFields = await _instrumentProminentFieldsRepository.QueryAsync(f => f.Isin.In(isins));
            _cache = (List<InstrumentProminentFieldsEntity>)instrumentProminentFields.Data;
            _lastFetch = DateTime.Now;
        }
            
        return _cache;
    }
}