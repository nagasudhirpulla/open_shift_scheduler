using FluentValidation;

namespace OSS.App.Shifts.Queries.GetShiftRoster
{
    public class GetShiftRosterQueryValidator : AbstractValidator<GetShiftRosterQuery>
    {
        public GetShiftRosterQueryValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
        }
    }
}
