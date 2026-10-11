using AutoDis.UseCases.Cars.GetCarCountries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoDis.WebApi.Controllers;

[ApiController]
[Route("api/v1/cars")]
public sealed class CarController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetCarCountriesQuery(), cancellationToken);
        return this.SendOkResult(result);
    }
}
