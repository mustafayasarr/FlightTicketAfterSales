using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicket.Domain.Models.Dtos;

public class FlightDto
{
    public required string AirlineName { get; set; }
    public required DateTime DepartureDateTime { get; set; }
    public required DateTime ArrivalDateTime { get; set; }
    public AirportDto OriginAirport { get; set; }
    public AirportDto DestinationAirport { get; set; }
}
