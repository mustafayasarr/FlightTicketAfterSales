namespace FlightTicket.Domain.Messages.Passenger.Response;

public class GetPassengerListResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}
