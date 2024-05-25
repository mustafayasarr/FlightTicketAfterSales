using FlightTicket.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicket.Domain.Messages.Flight.Queries.Response;

public class GetTicketListResponse
{
    public Guid Id { get; set; }
    public string PNR { get; set; }
    public PassengerDto Passenger { get; set; }
    public FlightDto Flight { get; set; }
}
