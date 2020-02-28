using OSS.Domain.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace OSS.Domain.Entities
{
    public class ShiftParticipationType : BaseEntity
    {
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
