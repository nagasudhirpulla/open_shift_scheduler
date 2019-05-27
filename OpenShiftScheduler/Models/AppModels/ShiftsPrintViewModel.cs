using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Models.AppModels
{
    public class ShiftsPrintViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public List<string> ShiftTypes { get; set; }

        // the shift participations for a date will in the same order as the shift types array
        public Dictionary<DateTime, List<List<string>>> ShiftParticipations { get; set; }
    }
}
