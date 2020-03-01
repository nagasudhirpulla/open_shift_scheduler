using OSS.Domain.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
namespace OSS.Domain.Entities
{
    // color picker - https://www.eyecon.ro/colorpicker/#about
    public class ShiftType : BaseEntity
    {
        public string Name { get; set; }
        public int StartOffsetHrs { get; set; }
        public int StartOffsetMins { get; set; }
        public int ShiftSequence { get; set; }
        // TODO implement this via fluent validation
        [NotMapped]
        public Color DisplayColor { get; set; } = Color.Purple;
        public string ColorString
        {
            get { return ColorUtils.ToHexValue(DisplayColor); }
            set { DisplayColor = ColorTranslator.FromHtml(value); }
        }
    }
}
