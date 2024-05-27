using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Interfaces;
using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Flight.Request;
using FlightTicket.Domain.Messages.Ticket.Response;
using FluentValidation;

namespace FlightTicket.Domain.Messages.Ticket.Request;

public class VoidTicketRequest : BaseValidationModel<VoidTicketRequest>, ICommand<VoidTicketResponse>
{
    public string TicketId { get; set; }
    public string PNR { get; set; }
}
public class VoidTicketRequestValidator : AbstractValidator<VoidTicketRequest>
{
    public VoidTicketRequestValidator()
    {
        RuleFor(f => f.TicketId)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty)
            .Must(ValidateBar).WithMessage(ValidationMessages.ValidateGuid);

        RuleFor(f => f.PNR)
             .NotNull().WithMessage(ValidationMessages.NotEmpty)
             .NotEmpty().WithMessage(ValidationMessages.NotEmpty);
    }
    private bool ValidateBar(string bar)
    {
        return Guid.TryParse(bar, out _);
    }
}
