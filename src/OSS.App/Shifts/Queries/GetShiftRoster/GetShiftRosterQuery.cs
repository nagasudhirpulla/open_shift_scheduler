using MediatR;

namespace OSS.App.Shifts.Queries.GetShiftRoster;

public class GetShiftRosterQuery : IRequest<ShiftRosterDTO>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
