using OSS.Domain.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace OSS.Domain.Entities;
public class ShiftParticipationType : BaseEntity
{
    public string Name { get; set; }
    public bool IsAbsence { get; set; }
    public bool IsBold { get; set; }

    [JsonIgnore]
    [NotMapped]
    public Color DisplayColor { get; set; } = Color.Purple;

    [DisplayName("Font Color")]
    public string ColorString
    {
        get { return ColorUtils.ToHexValue(DisplayColor); }
        set { DisplayColor = ColorTranslator.FromHtml(value); }
    }

    [DisplayName("Background Color")]
    public string BgClrString { get; set; }
}
