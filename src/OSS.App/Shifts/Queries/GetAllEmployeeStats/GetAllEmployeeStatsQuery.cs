using MediatR;

namespace OSS.App.Shifts.Queries.GetAllEmployeeStats;

public partial class GetAllEmployeeStatsQuery : IRequest<List<EmployeeStatsDTO>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
