using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Models.AppModels
{
    public class ShiftCycleItem
    {
        // make sure shift sequence is unique
        // shift type linking is cascade delete, set in db context
        public int ShiftCycleItemId { get; set; }

        [Range(0, 500)]
        public int ShiftSequence { get; set; }

        [Required]
        public int ShiftTypeId { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
