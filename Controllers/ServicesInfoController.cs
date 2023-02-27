using api.common;
using healthcheck.api.Application.Features.HealthCheckServicesInfo.Queries;
using healthcheck.api.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace healthcheck.api.Controllers;


public class ServicesInfoController : EasyControllerBase
{

    [HttpGet("Health-Check-Services-List")]
    public Task<IEnumerable<ServicesInfoDto>> GetHealthCheckServicesList()
            => Mediator.Send(new GetHealthCheckServicesInfoQuery());

}