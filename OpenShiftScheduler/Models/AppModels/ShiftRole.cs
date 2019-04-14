using System.ComponentModel.DataAnnotations;

namespace OpenShiftScheduler.Models.AppModels
{
    public class ShiftRole
    {
        public int ShiftRoleId { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        public string RoleName { get; set; }
    }
}
