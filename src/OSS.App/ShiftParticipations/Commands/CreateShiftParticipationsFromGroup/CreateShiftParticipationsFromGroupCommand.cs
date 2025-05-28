using MediatR;
using OSS.Domain.Entities;
using System.Collections.Generic;

namespace OSS.App.ShiftParticipations.Commands.CreateShiftParticipationsFromGroup;

public class CreateShiftParticipationsFromGroupCommand : IRequest<List<ShiftParticipation>>
{
    public int ShiftId { get; set; }
    public int ShiftGroupId { get; set; }        
}
