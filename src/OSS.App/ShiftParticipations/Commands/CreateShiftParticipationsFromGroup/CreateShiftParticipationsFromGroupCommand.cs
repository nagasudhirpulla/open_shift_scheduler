using MediatR;
using OSS.Domain.Entities;

namespace OSS.App.ShiftParticipations.Commands.CreateShiftParticipationsFromGroup;

public class CreateShiftParticipationsFromGroupCommand : IRequest<List<ShiftParticipation>>
{
    public int ShiftId { get; set; }
    public int ShiftGroupId { get; set; }
}
