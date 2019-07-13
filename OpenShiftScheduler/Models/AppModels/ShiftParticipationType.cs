using OpenShiftScheduler.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace OpenShiftScheduler.Models.AppModels
{
    public class ShiftParticipationType
    {
        public int ShiftParticipationTypeId { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        public bool IsAbsence { get; set; }

        public bool IsBold { get; set; }

        [NotMapped]
        public Color DisplayColor { get; set; } = Color.Purple;
        public string ColorString
        {
            get { return ColorUtils.ToHexValue(DisplayColor); }
            set { DisplayColor = ColorTranslator.FromHtml(value); }
        }

    }
}
