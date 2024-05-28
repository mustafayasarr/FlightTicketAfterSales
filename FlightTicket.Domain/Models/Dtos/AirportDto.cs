namespace FlightTicket.Domain.Models.Dtos;

public class AirportDto
{
    public AirportDto()
    {
        
    }
    public AirportDto(string airportCode,string airportName,string location)
    {
        AirportCode = airportCode;
        AirportName = airportName;
        Location = location;
    }
    public string AirportCode { get; set; }
    public string AirportName { get; set; }
    public string Location { get; set; }
}
