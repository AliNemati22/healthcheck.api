using api.common;
using healthcheck.api.Application.Features.WebServiceStatus.Queries;
using healthcheck.api.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace healthcheck.api.Controllers;


public class WebServicesConnectionCheckerController : EasyControllerBase
{

    [HttpGet("all-webservices-info")]
    public  Task<IEnumerable<WebServiceStatusDto>> GetAllWebServicesInfo(bool returnAll=false)
        => Mediator.Send(new GetWebServicesStatusQuery(returnAll));
}