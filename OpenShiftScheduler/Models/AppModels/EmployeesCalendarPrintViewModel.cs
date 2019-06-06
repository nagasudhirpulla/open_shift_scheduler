using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenShiftScheduler.Models.AppModels
{
    public class EmployeesCalendarPrintViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // the shift participations for a date will in the same order as the shift types array
        public Dictionary<String, List<string>> EmployeeShifts { get; set; }
    }
}
