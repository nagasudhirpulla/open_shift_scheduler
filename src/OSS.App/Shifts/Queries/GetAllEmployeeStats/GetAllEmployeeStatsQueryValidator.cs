using FluentValidation;

namespace OSS.App.Shifts.Queries.GetAllEmployeeStats;

public class GetAllEmployeeStatsQueryValidator : AbstractValidator<GetAllEmployeeStatsQuery>
{
    public GetAllEmployeeStatsQueryValidator()
    {
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
