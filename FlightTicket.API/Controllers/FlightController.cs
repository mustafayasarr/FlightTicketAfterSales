using FlightTicket.Domain.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FlightTicket.Domain.Messages.Ticket.Response;
using FlightTicket.Domain.Messages.Flight.Request;
using FlightTicket.Domain.Messages.Flight.Response;
using FlightTicket.Domain.Messages.Ticket.Request;
using FlightTicket.API.Filter;

namespace FlightTicket.API.Controllers;

[ModelValidator]

public class FlightController : BaseApiController
{
    public FlightController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("findflights")]
    [ProducesResponseType(typeof(Result<List<FindFlightResponse>>), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(Result))]
    public async Task<IActionResult> FindFlights([FromBody] FindFlightRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

   
}
