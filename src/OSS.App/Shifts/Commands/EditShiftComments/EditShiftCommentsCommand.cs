using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.App.Shifts.Commands.EditShiftComments
{
    public class EditShiftCommentsCommand : IRequest<bool>
    {
        public int ShiftId { get; set; }
        public string Comments { get; set; }
    }
}
