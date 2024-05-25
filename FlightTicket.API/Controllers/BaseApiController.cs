using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightTicket.API.Controllers;

[Route("v1/api/[controller]")]
[ApiController]
[Consumes("application/json")]
public abstract class BaseApiController : ControllerBase
{
    public readonly IMediator _mediator;
    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
