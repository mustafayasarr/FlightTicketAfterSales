namespace FlightTicket.Domain.Messages.Ticket.Response;

public class BookingResponse
{
    public BookingResponse(string pnr)
    {
        PNR= pnr;
    }
    public string PNR { get; set; }
}
