using OpenShiftScheduler.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Models.AppModels
{
    public class ShiftType
    {
        public int ShiftTypeId { get; set; }
        public string Name { get; set; }
        [Range(0, 23)]
        public int StartOffsetHrs { get; set; }
        [Range(0, 59)]
        public int StartOffsetMins { get; set; }
        [Range(0, 500)]
        public int RoasterSequence { get; set; }
        [Range(0, 500)]
        public int ShiftSequence { get; set; }
        [NotMapped]
        public Color DisplayColor { get; set; } = Color.Purple;
        public string ColorString
        {
            get { return ColorUtils.ToHexValue(DisplayColor); }
            set { DisplayColor = ColorTranslator.FromHtml(value); }
        }        
    }
}
