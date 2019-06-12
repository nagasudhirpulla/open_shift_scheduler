using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Models.AppModels
{
    /**
     * Description
     * ***********
     * Shift is an event in time. 
     * Multiple employees can participate in a shift. 
     * This info will be captured in another table as it as many-to-many relation.
     * 
     * Constraints
     * ***********
     * Shift Date and Shift Type need to be unique since, a shift type can occur only once in a day.
     * **/
    public class Shift
    {
        public int ShiftId { get; set; }

        public ShiftType ShiftType { get; set; }
        public int ShiftTypeId { get; set; }

        public DateTime ShiftDate { get; set; }

        public IList<ShiftParticipation> ShiftParticipations { get; set; }

        public string Comments { get; set; }
    }
}
