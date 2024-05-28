using FlightTicket.Infrastructure.Persistence;
using FlightTicket.Test.Mocks;
using FlightTicket.Application.Commands.Ticket;
using FlightTicket.Domain.Messages.Ticket.Request;
using Shouldly;
using FlightTicket.Test.MockData;
using FlightTicket.Domain.Models.Entities;
using FlightTicket.Application.Queries.Ticket;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace FlightTicket.Test.Services;

public class TicketTests
{
    private ApplicationDbContext context;
    public TicketTests()
    {
        context = DbContextMock.GetTicketMock();
    }

    #region GetList
    [Fact]
    public async Task GetList_ShouldBeGreaterThan_Any()
    {
        var handler = new GetTicketListQuery(context);
        var result = await handler.Handle(new GetTicketListRequest(), CancellationToken.None);
        result.Value.Count.ShouldBeGreaterThan(0);
    }

    #endregion

    #region VoidTicket
    [Theory]
    [InlineData("e6fd871d-ba7a-4518-9a8c-b0f322094a22", "TK-8C5SSFGC")]
    public async Task VoidTicket_ShouldBeTrue_IsSuccessTrue(string ticketId, string pnr)
    {
        var handler = new VoidTicketCommand(context);
        var result = await handler.Handle(new VoidTicketRequest { TicketId = ticketId, PNR = pnr }, CancellationToken.None);
        result.Success.ShouldBeTrue();
    }

    [Theory]
    [InlineData("6edde51d-1cc8-47b5-a5e9-e5f30c222f0b", "TK-8C5SSFGC")]
    [InlineData("e6fd871d-ba7a-4518-9a8c-b0f322094a22", "TK-41231233")]
    [InlineData("6edde51d-1cc8-47b5-a5e9-e5f30c222f0b", "TK-41231233")]

    public async Task VoidTicket_ShouldBeTrue_IsFailureTrue(string ticketId, string pnr)
    {
        var handler = new VoidTicketCommand(context);
        var result = await handler.Handle(new VoidTicketRequest { TicketId = ticketId, PNR = pnr }, CancellationToken.None);
        result.IsFailure.ShouldBeTrue();
    }

    [Theory]

    [InlineData("e6fd871d-ba7a-4518-9a8c-", "TK-8C5SSFGC")]
    [InlineData("e6fd871d-ba7a-4518-9a8c-b0f322094a22", "")]
    [InlineData("", "TK-8C5SSFGC")]

    [InlineData("e6fd871d-ba7a-4518-9a8c-b0f322094a22", null)]
    [InlineData(null, "TK-8C5SSFGC")]

    [InlineData("", "")]
    [InlineData(null, null)]

    public void VoidTicket_ShouldBe_ValidatorBeFalse(string? ticketId, string? pnr)
    {
        var request = new VoidTicketRequest { TicketId = ticketId, PNR = pnr };
        var validator = new VoidTicketRequestValidator();
        validator.Validate(request).IsValid.ShouldBeFalse();
    }

    #endregion

    #region Reissue
    [Theory]
    [InlineData("170d1532-26e1-4a1f-911c-6edfaf2b7a50", "TK-8GJSVA5", "f3d5a284-45c6-4e38-bef3-c698c5132436")]
    public async Task ReissueTicket_ShouldBe_IsSuccessTrue(string passengerId, string pnr, string newFlightId)
    {
        var handler = new ReissueTicketCommand(context);
        var result = await handler.Handle(new ReissueTicketRequest
        {
            PassengerId = passengerId,
            PNR = pnr,
            NewFlightId = newFlightId
        }, CancellationToken.None);

        result.Success.ShouldBeTrue();
    }

    [Theory]
    [InlineData("170d1532-26e1-4a1f-911c-6edfaf2b7a50", "TK-8GJSVA5", "243b4217-c271-49e7-8fa8-a1d282483192")]
    [InlineData("170d1532-26e1-4a1f-911c-6edfaf2b7a50", "TK-231234D", "f3d5a284-45c6-4e38-bef3-c698c5132436")]
    [InlineData("243b4217-c271-49e7-8fa8-a1d282483192", "12DSF123.!", "f3d5a284-45c6-4e38-bef3-c698c5132436")]

    public async Task ReissueTicket_ShouldBeTrue_IsFailureTrue(string passengerId, string pnr, string newFlightId)
    {
        var handler = new ReissueTicketCommand(context);
        var result = await handler.Handle(new ReissueTicketRequest
        {
            PassengerId = passengerId,
            PNR = pnr,
            NewFlightId = newFlightId
        }, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
    }

    [Theory]

    [InlineData("170d1532-26e1-4a1f-911c-3123123", "TK-8GJSVA5", "243b4217-c271-49e7-8fa8-a1d282483192")]
    [InlineData("170d1532-26e1-4a1f-911c-6edfaf2b7a50", null, "243b4217-c271-49e7-8fa8-a1d282483192")]
    [InlineData("170d1532-26e1-4a1f-911c-6edfaf2b7a50", "TK-8GJSVA5", "-c271-49e7-8fa8-a1d282483192")]


    [InlineData("", "TK-8GJSVA5", "243b4217-c271-49e7-8fa8-a1d282483192")]
    [InlineData("170d1532-26e1-4a1f-911c-6edfaf2b7a50", "", "243b4217-c271-49e7-8fa8-a1d282483192")]
    [InlineData("170d1532-26e1-4a1f-911c-6edfaf2b7a50", "TK-8GJSVA5", "")]

    [InlineData("", "", "")]
    [InlineData(null, null, null)]

    public void ReissueTicket_ShouldBe_ValidatorBeFalse(string? passengerId, string? pnr, string? newFlightId)
    {
        var request = new ReissueTicketRequest
        {
            PassengerId = passengerId,
            PNR = pnr,
            NewFlightId = newFlightId
        };
        var validator = new ReissueTicketRequestValidator();
        validator.Validate(request).IsValid.ShouldBeFalse();
    }
    #endregion

    #region Booking
    [Theory]
    [InlineData("f3d5a284-45c6-4e38-bef3-c698c5132436", "4e450608-ed61-4725-a192-84e0edd8b0b5", false, null, null, null)]
    [InlineData("f3d5a284-45c6-4e38-bef3-c698c5132436", null, true, "Mustafa", "Yaşar", "1986-11-17")]

    public async Task Booking_ShouldBeTrue_IsSuccessTrue(string flightId, string? passengerId, bool isNewPassenger, string? passengerName, string? passengerLastName, DateTime birthDate)
    {
        var handler = new BookingCommand(context);
        var result = await handler.Handle(new BookingRequest
        {
            FlightId = flightId,
            PassengerId = passengerId,
            IsNewPassenger = isNewPassenger,
            PassengerName = passengerName,
            PassengerLastName = passengerLastName,
            BirthDate = birthDate
        }, CancellationToken.None);
        result.Success.ShouldBeTrue();
    }

    [Theory]
    [InlineData("755c0ca0-7f08-4874-8847-3ad3c009b0e1", "4e450608-ed61-4725-a192-84e0edd8b0b5", false, null, null, null)]
    [InlineData("f3d5a284-45c6-4e38-bef3-c698c5132436", "755c0ca0-7f08-4874-8847-3ad3c009b0e1", false, null, null, null)]

    public async Task Booking_ShouldBeTrue_IsFailureTrue(string flightId, string? passengerId, bool isNewPassenger, string? passengerName, string? passengerLastName, DateTime birthDate)
    {
        var handler = new BookingCommand(context);
        var result = await handler.Handle(new BookingRequest
        {
            FlightId = flightId,
            PassengerId = passengerId,
            IsNewPassenger = isNewPassenger,
            PassengerName = passengerName,
            PassengerLastName = passengerLastName,
            BirthDate = birthDate
        }, CancellationToken.None);
        result.IsFailure.ShouldBeTrue();
    }

    [Theory]
    [InlineData("755c0ca0-7f08-4874-8847-23123123", "4e450608-ed61-4725-a192-asdas", false, null, null, null)]
    [InlineData("755c0ca0-7f08-4874-8847-", "4e450608-ed61-4725-a192-", false, null, null, null)]
    [InlineData("f3d5a284-45c6-4e38-bef3-c698c5132436", "4e450608-ed61-4725-a192-84e0edd8b0b5", true, null, null, null)]
    [InlineData(null, null, true, null, null, null)]
    [InlineData(null, null, false, null, null, null)]
    [InlineData("", "", false, null, null, null)]

    public void Booking_ShouldBe_ValidatorBeFalse(string? flightId, string? passengerId, bool isNewPassenger, string? passengerName, string? passengerLastName, DateTime birthDate)
    {
        var request = new BookingRequest
        {
            FlightId = flightId,
            PassengerId = passengerId,
            IsNewPassenger = isNewPassenger,
            PassengerName = passengerName,
            PassengerLastName = passengerLastName,
            BirthDate = birthDate
        };
        var validator = new BookingRequestValidator();
        validator.Validate(request).IsValid.ShouldBeFalse();
    }
    #endregion

}

