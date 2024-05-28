using FlightTicket.Domain.Models.Entities;
using FlightTicket.Infrastructure.Persistence;
using FlightTicket.Test.MockData;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Linq.Expressions;

namespace FlightTicket.Test.Mocks;

public class DbContextMock
{
    public static TContext GetMock<TData, TContext>(List<TData> lstData, Expression<Func<TContext, DbSet<TData>>> dbSetSelectionExpression) where TData : class where TContext : DbContext
    {
        IQueryable<TData> lstDataQueryable = lstData.AsQueryable().BuildMock();
        Mock<DbSet<TData>> dbSetMock = new();
        Mock<TContext> dbContext = new();

        dbSetMock.As<IQueryable<TData>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
        dbSetMock.As<IQueryable<TData>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
        dbSetMock.As<IQueryable<TData>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
        dbSetMock.As<IQueryable<TData>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
        dbSetMock.Setup(x => x.Add(It.IsAny<TData>())).Callback<TData>(lstData.Add);
        dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<TData>>())).Callback<IEnumerable<TData>>(lstData.AddRange);
        dbSetMock.Setup(x => x.Remove(It.IsAny<TData>())).Callback<TData>(t => lstData.Remove(t));
        dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<TData>>())).Callback<IEnumerable<TData>>(ts =>
        {
            foreach (var t in ts) { lstData.Remove(t); }
        });


        dbContext.Setup(dbSetSelectionExpression).Returns(dbSetMock.Object);

        return dbContext.Object;
    }


    public static ApplicationDbContext GetTicketMock()
    {
        IQueryable<TicketEntity> lstDataQueryable = MockListData.TicketList().AsQueryable().BuildMock();
        Mock<DbSet<TicketEntity>> dbSetMock = new();
        Mock<ApplicationDbContext> dbContext = new();
        List<TicketEntity> lstData = MockListData.TicketList();
        dbSetMock.As<IQueryable<TicketEntity>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
        dbSetMock.As<IQueryable<TicketEntity>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
        dbSetMock.As<IQueryable<TicketEntity>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
        dbSetMock.As<IQueryable<TicketEntity>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
        dbSetMock.Setup(x => x.Add(It.IsAny<TicketEntity>())).Callback<TicketEntity>(lstData.Add);
        dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<TicketEntity>>())).Callback<IEnumerable<TicketEntity>>(lstData.AddRange);
        dbSetMock.Setup(x => x.Remove(It.IsAny<TicketEntity>())).Callback<TicketEntity>(t => lstData.Remove(t));
        dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<TicketEntity>>())).Callback<IEnumerable<TicketEntity>>(ts =>
        {
            foreach (var t in ts) { lstData.Remove(t); }
        });


        dbContext.Setup(a => a.Tickets).Returns(dbSetMock.Object);
        dbContext.Setup(a => a.Flights).Returns(TicketByFlightMock().Object);
        dbContext.Setup(a => a.Passengers).Returns(TicketByPassengerMock().Object);

        return dbContext.Object;

    }
    private static Mock<DbSet<FlightEntity>> TicketByFlightMock()
    {
        IQueryable<FlightEntity> lstDataQueryable = MockListData.FlightList().AsQueryable().BuildMock();
        Mock<DbSet<FlightEntity>> dbSetMock = new();
        Mock<ApplicationDbContext> dbContext = new();
        List<FlightEntity> lstData = MockListData.FlightList();
        dbSetMock.As<IQueryable<FlightEntity>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
        dbSetMock.As<IQueryable<FlightEntity>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
        dbSetMock.As<IQueryable<FlightEntity>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
        dbSetMock.As<IQueryable<FlightEntity>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
        dbSetMock.Setup(x => x.Add(It.IsAny<FlightEntity>())).Callback<FlightEntity>(lstData.Add);
        dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<FlightEntity>>())).Callback<IEnumerable<FlightEntity>>(lstData.AddRange);
        dbSetMock.Setup(x => x.Remove(It.IsAny<FlightEntity>())).Callback<FlightEntity>(t => lstData.Remove(t));
        dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<FlightEntity>>())).Callback<IEnumerable<FlightEntity>>(ts =>
        {
            foreach (var t in ts) { lstData.Remove(t); }
        });

        return dbSetMock;
    }
    private static Mock<DbSet<PassengerEntity>> TicketByPassengerMock()
    {
        IQueryable<PassengerEntity> lstDataQueryable = MockListData.PassengerList().AsQueryable().BuildMock();
        Mock<DbSet<PassengerEntity>> dbSetMock = new();
        Mock<ApplicationDbContext> dbContext = new();
        List<PassengerEntity> lstData = MockListData.PassengerList();
        dbSetMock.As<IQueryable<PassengerEntity>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
        dbSetMock.As<IQueryable<PassengerEntity>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
        dbSetMock.As<IQueryable<PassengerEntity>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
        dbSetMock.As<IQueryable<PassengerEntity>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
        dbSetMock.Setup(x => x.Add(It.IsAny<PassengerEntity>())).Callback<PassengerEntity>(lstData.Add);
        dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<PassengerEntity>>())).Callback<IEnumerable<PassengerEntity>>(lstData.AddRange);
        dbSetMock.Setup(x => x.Remove(It.IsAny<PassengerEntity>())).Callback<PassengerEntity>(t => lstData.Remove(t));
        dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<PassengerEntity>>())).Callback<IEnumerable<PassengerEntity>>(ts =>
        {
            foreach (var t in ts) { lstData.Remove(t); }
        });

        return dbSetMock;
    }


    public static ApplicationDbContext GetFlightMock()
    {
        IQueryable<FlightEntity> lstDataQueryable = MockListData.FlightList().AsQueryable().BuildMock();
        Mock<DbSet<FlightEntity>> dbSetMock = new();
        Mock<ApplicationDbContext> dbContext = new();
        List<FlightEntity> lstData = MockListData.FlightList();
        dbSetMock.As<IQueryable<FlightEntity>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
        dbSetMock.As<IQueryable<FlightEntity>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
        dbSetMock.As<IQueryable<FlightEntity>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
        dbSetMock.As<IQueryable<FlightEntity>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
        dbSetMock.Setup(x => x.Add(It.IsAny<FlightEntity>())).Callback<FlightEntity>(lstData.Add);
        dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<FlightEntity>>())).Callback<IEnumerable<FlightEntity>>(lstData.AddRange);
        dbSetMock.Setup(x => x.Remove(It.IsAny<FlightEntity>())).Callback<FlightEntity>(t => lstData.Remove(t));
        dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<FlightEntity>>())).Callback<IEnumerable<FlightEntity>>(ts =>
        {
            foreach (var t in ts) { lstData.Remove(t); }
        });


        dbContext.Setup(a => a.Flights).Returns(dbSetMock.Object);
        dbContext.Setup(a => a.Airports).Returns(FlightByAirportMock().Object);

        return dbContext.Object;

    }

    private static Mock<DbSet<AirportEntity>> FlightByAirportMock()
    {
        IQueryable<AirportEntity> lstDataQueryable = MockListData.AirportList().AsQueryable().BuildMock();
        Mock<DbSet<AirportEntity>> dbSetMock = new();
        Mock<ApplicationDbContext> dbContext = new();
        List<AirportEntity> lstData = MockListData.AirportList();
        dbSetMock.As<IQueryable<AirportEntity>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
        dbSetMock.As<IQueryable<AirportEntity>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
        dbSetMock.As<IQueryable<AirportEntity>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
        dbSetMock.As<IQueryable<AirportEntity>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
        dbSetMock.Setup(x => x.Add(It.IsAny<AirportEntity>())).Callback<AirportEntity>(lstData.Add);
        dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<AirportEntity>>())).Callback<IEnumerable<AirportEntity>>(lstData.AddRange);
        dbSetMock.Setup(x => x.Remove(It.IsAny<AirportEntity>())).Callback<AirportEntity>(t => lstData.Remove(t));
        dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<AirportEntity>>())).Callback<IEnumerable<AirportEntity>>(ts =>
        {
            foreach (var t in ts) { lstData.Remove(t); }
        });

        return dbSetMock;
    }



}
