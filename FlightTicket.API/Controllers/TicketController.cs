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

    [HttpPost("booking")]
    [ProducesResponseType(typeof(Result<BookingResponse>), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(Result))]
    public async Task<IActionResult> Booking([FromBody] BookingRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("void-ticket")]
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(Result))]
    public async Task<IActionResult> VoidTicket([FromBody] VoidTicketRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("reissue-ticket")]
    [ProducesResponseType(typeof(Result<ReissueTicketResponse>), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(Result))]
    public async Task<IActionResult> ReissueTicket([FromBody] ReissueTicketRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}
