using api.common;
using domain.Entities;
using healthcheck.api.Application.Features.Symbol.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace healthcheck.api.Controllers;


public class SymbolController : EasyControllerBase
{
    
    //[HttpGet("[action]")]
    //public  async Task<ActionResult<IEnumerable<SymbolEntity>>> GetAllSymbols()
    //{
    //    var result = await _mediator.Send(new GetSymbolesQuery());
    //    return Ok(result);
    //}

    //[HttpGet("[action]")]
    //public async Task<ActionResult<IEnumerable<SymbolEntity>>> GetAllActiveSymbols()
    //{
    //    var result = await _mediator.Send(new GetActiveSymbolesQuery());
    //    return Ok(result);
    //}

    //[HttpGet("[action]")]
    //public async Task<ActionResult<IEnumerable<SymbolEntity>>> GetSymbolsByIsin(string symbolIsin)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    var result = await _mediator.Send(new GetOneSymbolesByIsinQuery
    //    {
    //        Isin = symbolIsin
    //    });

    //    return Ok(result);
    //}
}