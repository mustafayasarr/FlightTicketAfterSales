using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Flight.Request;
using FlightTicket.Domain.Messages.Ticket.Response;
using FluentValidation;

namespace FlightTicket.Domain.Messages.Ticket.Request;

public class BookingRequest:ICommand<BookingResponse>
{
    public string FlightId { get; set; }
    public string? PassengerId { get; set; }
    public bool IsNewPassenger { get; set; } = false;
    public string? PassengerName { get; set; }
    public string? PassengerLastName{ get; set; }
    public DateTime? BirthDate { get; set; }
}
public class FindFlightRequestValidator : AbstractValidator<FindFlightRequest>
{
    public FindFlightRequestValidator()
    {
        RuleFor(f => f.OriginAirportId)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty)
            .Must(ValidateBar).WithMessage(ValidationMessages.ValidateGuid);

        RuleFor(f => f.DestinationAirportId)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty)
            .Must(ValidateBar).WithMessage(ValidationMessages.ValidateGuid);


        RuleFor(f => f.DestinationAirportId)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty)
            .Must(ValidateBar).WithMessage(ValidationMessages.ValidateGuid);
    }
    private bool ValidateBar(string bar)
    {
        return Guid.TryParse(bar, out _);
    }
}
