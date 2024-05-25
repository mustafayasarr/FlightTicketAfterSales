namespace FlightTicket.Domain.Models.Dtos;

public class AirportDto
{
    public required string AirportCode { get; set; }
    public required string AirportName { get; set; }
    public required string Location { get; set; }
}
