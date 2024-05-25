using FlightTicket.Domain.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FlightTicket.Domain.Messages.Airport.Queries.Response;
using FlightTicket.Domain.Messages.Airport.Queries.Request;

namespace FlightTicket.API.Controllers;

public class AirportController : BaseApiController
{
    public AirportController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(Result<PaginatedList<GetAirportListResponse>>), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(Result))]
    public async Task<IActionResult> AirportList([FromQuery]GetAirportListRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}
