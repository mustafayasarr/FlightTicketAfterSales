using FlightTicket.Application.Queries.Airport;
using FlightTicket.Application.Queries.Passenger;
using FlightTicket.Domain.Messages.Airport.Request;
using FlightTicket.Domain.Messages.Passenger.Request;
using FlightTicket.Domain.Messages.Passenger.Response;
using FlightTicket.Domain.Models.Entities;
using FlightTicket.Infrastructure.Persistence;
using FlightTicket.Test.MockData;
using FlightTicket.Test.Mocks;
using Shouldly;

namespace FlightTicket.Test.Services;

public class PassengerTests
{
    private ApplicationDbContext context;
    public PassengerTests()
    {
        context = DbContextMock.GetMock<PassengerEntity, ApplicationDbContext>(MockListData.PassengerList(), x => x.Passengers);
    }

    [Fact]
    public async Task GetList_ShouldBeGreaterThan_Any()
    {
        var handler = new GetPassengerListQuery(context);
        var result = await handler.Handle(new GetPassengerListRequest(), CancellationToken.None);
        result.Value.Count.ShouldBeGreaterThan(0);
    }
}
