using System;
using System.ComponentModel.DataAnnotations;

namespace OpenShiftScheduler.Models.AppModels
{
    public class LeaveApplyViewModel
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string RequesterRemarks { get; set; }
    }
}
