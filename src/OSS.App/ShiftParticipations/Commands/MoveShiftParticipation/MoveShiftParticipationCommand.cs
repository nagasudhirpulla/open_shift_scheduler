using MediatR;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.App.ShiftParticipations.Commands.MoveShiftParticipation
{
    public class MoveShiftParticipationCommand : IRequest<Shift>
    {
        public int Direction { get; set; }
        public int ShiftParticipationId { get; set; }
    }
}
