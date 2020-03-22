using MediatR;
using System;

namespace OSS.App.ShiftParticipations.Commands.FollowShiftParticipation
{
    public class FollowShiftParticipationCommand : IRequest<bool>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TargetEmployeeId { get; set; }
        public int? TargetParticipationTypeId { get; set; }
        public int NewParticipationTypeId { get; set; }
        public string NewEmployeeId { get; set; }
    }
}
