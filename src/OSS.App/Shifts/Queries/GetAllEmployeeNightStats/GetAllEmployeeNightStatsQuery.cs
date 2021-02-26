using MediatR;
using System;
using System.Collections.Generic;

namespace OSS.App.Shifts.Queries.GetAllEmployeeNightStats
{
    public partial class GetAllEmployeeNightStatsQuery : IRequest<List<EmployeeNightStatsDTO>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
