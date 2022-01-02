using MediatR;

namespace OSS.App.ShiftParticipations.Commands.FollowShiftParticipation;

public class FollowShiftParticipationCommand : IRequest<bool>
{
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public string TargetEmployeeId { get; set; }
    public int? TargetParticipationTypeId { get; set; }
    public int NewParticipationTypeId { get; set; }
    public string NewEmployeeId { get; set; }
    public int NewParticipationSequence { get; set; } = 0;
}
