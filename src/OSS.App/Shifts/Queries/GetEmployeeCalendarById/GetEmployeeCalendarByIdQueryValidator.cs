using FluentValidation;

namespace OSS.App.Shifts.Queries.GetEmployeeCalendarById;

public class GetEmployeeCalendarByIdQueryValidator : AbstractValidator<GetEmployeeCalendarByIdQuery>
{
    public GetEmployeeCalendarByIdQueryValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty();
    }
}
