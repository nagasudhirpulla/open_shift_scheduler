using FluentValidation;

namespace OSS.App.LeaveRequests.Queries.GetLeaveRequestsByEmpId;
public class GetLeaveRequestsByEmpIdQueryValidator : AbstractValidator<GetLeaveRequestsByEmpIdQuery>
{
    public GetLeaveRequestsByEmpIdQueryValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
    }
}