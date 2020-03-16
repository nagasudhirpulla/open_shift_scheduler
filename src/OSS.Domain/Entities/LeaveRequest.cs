using System;
using System.Collections.Generic;

namespace OSS.Domain.Entities
{
    public class LeaveRequest : AuditableEntity
    {
        public LeaveRequest()
        {
            LeaveRequestComments = new HashSet<LeaveRequestComment>();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ApplicationUser Employee { get; set; }
        public string EmployeeId { get; set; }

        public string Remarks { get; set; }

        public bool IsExecuted { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ExecutedAt { get; set; }

        public ICollection<LeaveRequestComment> LeaveRequestComments { get; private set; }
    }
}
