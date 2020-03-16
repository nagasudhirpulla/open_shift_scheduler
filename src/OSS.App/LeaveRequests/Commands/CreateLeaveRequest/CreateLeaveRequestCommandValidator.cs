using FluentValidation;
using System;

namespace OSS.App.LeaveRequests.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
    {
        public CreateLeaveRequestCommandValidator()
        {
            RuleFor(x => x.LeaveRequest.StartDate).Must(BeAValidDate).WithMessage("Start date is required");
            RuleFor(x => x.LeaveRequest.EndDate).Must(BeAValidDate).WithMessage("End date is required");
            RuleFor(x => x.LeaveRequest.Remarks).NotEmpty();
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}
