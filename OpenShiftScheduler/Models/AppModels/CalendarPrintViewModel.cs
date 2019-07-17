using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenShiftScheduler.Models.AppModels
{
    public class CalendarPrintViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        // Shift participation types for decorating the calendar
        public List<ShiftParticipationType> ShiftParticipationTypes { get; set; }

        // Shift types for decorating the calendar
        public List<ShiftType> ShiftTypes { get; set; }
        
        // Shift participations for populating the calendar
        public List<ShiftParticipation> EmployeeShiftParticipations { get; set; }

        // Calendar Events
        public List<CalendarEventViewModel> CalendarEvents { get; set; }
    }
}
