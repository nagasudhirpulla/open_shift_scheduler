using MediatR;
using OSS.Domain.Entities;

namespace OSS.App.ShiftParticipations.Commands.MoveShiftParticipation;

public class MoveShiftParticipationCommand : IRequest<Shift>
{
    public int Direction { get; set; }
    public int ShiftParticipationId { get; set; }
}
