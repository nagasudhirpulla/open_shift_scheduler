using System;
using System.Collections.Generic;

namespace OSS.Domain.Entities
{
    public class Shift : BaseEntity
    {
        public Shift()
        {
            ShiftParticipations = new HashSet<ShiftParticipation>();
        }

        public ShiftType ShiftType { get; set; }
        public int ShiftTypeId { get; set; }

        public DateTime ShiftDate { get; set; }

        public ICollection<ShiftParticipation> ShiftParticipations { get; private set; }

        public string Comments { get; set; }
    }
}
