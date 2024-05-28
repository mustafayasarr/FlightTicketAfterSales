namespace FlightTicket.Domain.Messages.Ticket.Response;

public class ReissueTicketResponse
{
    public ReissueTicketResponse(string pnr)
    {
        PNR = pnr;
    }
    public string PNR { get; set; }
}
