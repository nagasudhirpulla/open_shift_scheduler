using FluentValidation;

namespace OSS.App.ShiftParticipations.Commands.FollowShiftParticipation;

public class FollowShiftParticipationCommandValidator : AbstractValidator<FollowShiftParticipationCommand>
{
    public FollowShiftParticipationCommandValidator()
    {
        RuleFor(x => x.NewParticipationSequence).GreaterThanOrEqualTo(x => 0).WithMessage("New Participation Sequence should be >= 0");
        RuleFor(x => x.StartDate).Must(BeAValidDate).WithMessage("Start date is required");
        RuleFor(x => x.EndDate).Must(BeAValidDate).WithMessage("End date is required");
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("Start Date should be less than End Date");
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default);
    }
}
