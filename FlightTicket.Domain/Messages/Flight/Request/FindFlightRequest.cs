using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Interfaces;
using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Flight.Response;
using FlightTicket.Domain.Models.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace FlightTicket.Domain.Messages.Flight.Request;

public class FindFlightRequest : BaseValidationModel<FindFlightRequest>, ICommand<List<FindFlightResponse>>
{
    public string OriginAirportId { get; set; }
    public string DestinationAirportId { get; set; }
    public DateTime? DepartureDate { get; set; }
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
