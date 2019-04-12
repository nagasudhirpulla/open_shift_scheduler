using System.ComponentModel.DataAnnotations;

namespace OpenShiftScheduler.Models.AppModels
{
    public class Gender
    {
        public int GenderId { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
