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
public class BookingRequestValidator : AbstractValidator<BookingRequest>
{
    public BookingRequestValidator()
    {
        RuleFor(f => f.FlightId)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty)
            .Must(ValidateBar).WithMessage(ValidationMessages.ValidateGuid);

        RuleFor(f => f.PassengerId)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty)
            .Must(ValidateBar).WithMessage(ValidationMessages.ValidateGuid);


        RuleFor(f => f.PassengerName)
             .NotNull().WithMessage(ValidationMessages.NotEmpty).When(a => a.IsNewPassenger)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty).When(a => a.IsNewPassenger);

        RuleFor(f => f.PassengerLastName)
            .NotNull().WithMessage(ValidationMessages.NotEmpty).When(a => a.IsNewPassenger)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty).When(a => a.IsNewPassenger);

        RuleFor(f => f.BirthDate)
           .NotNull().WithMessage(ValidationMessages.NotEmpty).When(a => a.IsNewPassenger)
           .NotEmpty().WithMessage(ValidationMessages.NotEmpty).When(a => a.IsNewPassenger);
    }
    private bool ValidateBar(string bar)
    {
        return Guid.TryParse(bar, out _);
    }
}
