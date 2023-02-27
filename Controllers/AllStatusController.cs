using api.common;
using healthcheck.api.Application.Features.AllStatus.Queries;
using healthcheck.api.Application.Features.MarketData.Queries;
using healthcheck.api.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace healthcheck.api.Controllers;



public class AllStatusController : EasyControllerBase
{
    
    [HttpGet("all-status")]
    public  Task<AllStatusDto> GetAllStatus(bool returnAll = false)
         =>  Mediator.Send(new GetAllStatusQuery(returnAll));
}