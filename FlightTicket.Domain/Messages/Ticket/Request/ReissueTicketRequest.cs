using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Interfaces;
using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Flight.Request;
using FlightTicket.Domain.Messages.Ticket.Response;
using FluentValidation;

namespace FlightTicket.Domain.Messages.Ticket.Request;

public class ReissueTicketRequest : BaseValidationModel<ReissueTicketRequest>, ICommand<ReissueTicketResponse>
{
    public string PassengerId { get; set; }
    public  string PNR { get; set; }
    public string NewFlightId { get; set; }
}
public class ReissueTicketRequestValidator : AbstractValidator<ReissueTicketRequest>
{
    public ReissueTicketRequestValidator()
    {
        RuleFor(f => f.PassengerId)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty)
            .Must(ValidateBar).WithMessage(ValidationMessages.ValidateGuid);

        RuleFor(f => f.PNR)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty);

        RuleFor(f => f.NewFlightId)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty)
            .Must(ValidateBar).WithMessage(ValidationMessages.ValidateGuid);
    }
    private bool ValidateBar(string bar)
    {
        return Guid.TryParse(bar, out _);
    }
}
