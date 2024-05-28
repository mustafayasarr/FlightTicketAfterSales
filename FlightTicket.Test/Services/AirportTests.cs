using FlightTicket.Application.Queries.Airport;
using FlightTicket.Domain.Messages.Airport.Request;
using FlightTicket.Domain.Models.Entities;
using FlightTicket.Infrastructure.Persistence;
using FlightTicket.Test.MockData;
using FlightTicket.Test.Mocks;
using Shouldly;

namespace FlightTicket.Test.Services;

public class AirportTests
{
    private ApplicationDbContext context;
    public AirportTests()
    {
        context= DbContextMock.GetMock<AirportEntity, ApplicationDbContext>(MockListData.AirportList(), x => x.Airports);
    }
    [Fact]
    public async Task GetList_ShouldBeGreaterThan_Any()
    {
        var handler = new GetAirportListQuery(context);
        var result = await handler.Handle(new GetAirportListRequest(), CancellationToken.None);
        result.Value.PageContents.Count.ShouldBeGreaterThan(0);
    }
    [Fact]
    public async Task GetList_ShouldBe_Ankara()
    {
        var handler = new GetAirportListQuery(context);
        var result = await handler.Handle(new GetAirportListRequest { Find = "ankara" }, CancellationToken.None);
        result.Value.PageContents.First().AirportName.ShouldBe(AirportsMockData.DestinationAirport().AirportName);
    }
}
