using FlightTicket.Application.Commands.Flight;
using FlightTicket.Application.Queries.Airport;
using FlightTicket.Domain.Messages.Airport.Request;
using FlightTicket.Domain.Messages.Flight.Request;
using FlightTicket.Domain.Messages.Ticket.Request;
using FlightTicket.Domain.Models.Entities;
using FlightTicket.Infrastructure.Persistence;
using FlightTicket.Test.MockData;
using FlightTicket.Test.Mocks;
using Shouldly;

namespace FlightTicket.Test.Services;

public class FlightTests
{
    private ApplicationDbContext context;
    public FlightTests()
    {
        context = DbContextMock.GetFlightMock();
    }

    [Theory]
    [InlineData("28807285-5664-4761-9c29-e37efcfb9c08", "b599d1f9-84cc-46c9-9852-9c4f5bad0fbd")]
    public async Task FindFlight_ShouldBeGreaterThan_Any(string originAirportId, string destinationAirportId)
    {
        DateTime departureDate= DateTime.Now.AddDays(10);
        var handler = new FindFlightsCommand(context);
        var result = await handler.Handle(new FindFlightRequest
        {
            OriginAirportId = originAirportId,
            DestinationAirportId = destinationAirportId,
            DepartureDate = departureDate
        }, CancellationToken.None);

        result.Value.Count.ShouldBeGreaterThan(0);
    }

    [Theory]
    [InlineData("355e1fed-997b-479a-baa8-1c439a20bcd6", "b599d1f9-84cc-46c9-9852-9c4f5bad0fbd","2024-11-30")]
    [InlineData("28807285-5664-4761-9c29-e37efcfb9c08", "355e1fed-997b-479a-baa8-1c439a20bcd6", "2024-11-30")]
    [InlineData("6fdccfff-1eb2-4f98-8ad9-1ec8c4d566c0", "355e1fed-997b-479a-baa8-1c439a20bcd6", "2022-11-30")]

    public async Task FindFlight_ShouldBeGreaterThan_Expect0(string originAirportId, string destinationAirportId,DateTime departureDate)
    {
        var handler = new FindFlightsCommand(context);
        var result = await handler.Handle(new FindFlightRequest
        {
            OriginAirportId = originAirportId,
            DestinationAirportId = destinationAirportId,
            DepartureDate = departureDate
        }, CancellationToken.None);

        result.Value.Count.ShouldBe(0);
    }

    [Theory]
    [InlineData("355e1fed-997b-479a-baa8-1c439a20bcd6", "b599d1f9-84cc-46c9-9852-", "2024-06-15")]
    [InlineData("28807285-5664-4761-9c29-", "355e1fed-997b-479a-baa8-1c439a20bcd6","2024-06-15")]
    [InlineData("", "","2024-06-15")]

    public void FindFlight_ShouldBe_ValidatorBeFalse(string originAirportId, string destinationAirportId,DateTime departureDate)
    {
        var request = new FindFlightRequest
        {
            OriginAirportId = originAirportId,
            DestinationAirportId = destinationAirportId,
            DepartureDate = departureDate
        };
        var validator = new FindFlightRequestValidator();
        validator.Validate(request).IsValid.ShouldBeFalse();
    }
}
