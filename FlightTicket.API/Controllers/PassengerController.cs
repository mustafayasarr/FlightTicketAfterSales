using FlightTicket.Domain.Messages.Flight.Request;
using FlightTicket.Domain.Messages.Flight.Response;
using FlightTicket.Domain.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FlightTicket.Domain.Messages.Passenger.Response;
using FlightTicket.Domain.Messages.Passenger.Request;

namespace FlightTicket.API.Controllers;

public class PassengerController : BaseApiController
{
    public PassengerController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(Result<List<GetPassengerListResponse>>), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(Result))]
    public async Task<IActionResult> GetPassengerList()
    {
        return Ok(await _mediator.Send(new GetPassengerListRequest()));
    }
}
