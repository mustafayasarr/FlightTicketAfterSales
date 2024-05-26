using FlightTicket.Domain.Messages;
using FlightTicket.Domain.Messages.Ticket.Request;
using FlightTicket.Domain.Messages.Ticket.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlightTicket.API.Controllers;

public class TicketController : BaseApiController
{
    public TicketController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(Result<List<GetTicketListResponse>>), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(Result))]
    public async Task<IActionResult> TicketList()
    {
        return Ok(await _mediator.Send(new GetTicketListRequest()));
    }
}
