using System.ComponentModel.DataAnnotations;

namespace OpenShiftScheduler.Models.AppModels
{
    public class ShiftSkill
    {
        public int ShiftSkillId { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
