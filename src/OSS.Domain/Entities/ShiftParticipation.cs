namespace OSS.Domain.Entities
{
    public class ShiftParticipation : BaseEntity
    {
        /**
        * Description
        * ***********
        * The information about the employees participation in a particular shift is stored in this table.
        * This acts as a many-to-many relation info storing table for the two tables Employee and Shift
        * 
        * Constraints
        * ***********
        * The Employee, Shift columns combo needs to be unique, otherwise there will be data duplication.
        * Currently, there is no constraint that limits the employee to participation in only one shift per day.
        * **/
        public ApplicationUser Employee { get; set; }
        public int EmployeeId { get; set; }

        public Shift Shift { get; set; }
        public int ShiftId { get; set; }

        public ShiftParticipationType ShiftParticipationType { get; set; }
        public int? ShiftParticipationTypeId { get; set; }

        public int ParticipationSequence { get; set; }
    }
}
