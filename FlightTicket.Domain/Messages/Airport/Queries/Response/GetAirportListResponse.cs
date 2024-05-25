using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicket.Domain.Messages.Airport.Queries.Response
{
    public class GetAirportListResponse
    {
        public Guid Id { get; set; }
        public required string AirportCode { get; set; }
        public required string AirportName { get; set; }
        public required string Location { get; set; }
    }
}
