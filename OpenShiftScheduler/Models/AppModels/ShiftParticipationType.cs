using System.ComponentModel.DataAnnotations;


namespace OpenShiftScheduler.Models.AppModels
{
    public class ShiftParticipationType
    {
        public int ShiftParticipationTypeId { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        public bool IsAbsence { get; set; }
    }
}
