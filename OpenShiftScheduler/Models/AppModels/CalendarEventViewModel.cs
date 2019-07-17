using System;
using System.Collections.Generic;

namespace OpenShiftScheduler.Models.AppModels
{
    public class CalendarEventViewModel
    {
        public DateTime ShiftDate { get; set; }
        public List<string> EventTextClasses { get; set; }
        public string EventTitle { get; set; }
        public string TitleColor { get; set; }
        public string TitleBgColor { get; set; }
        public string TooltipText { get; set; }
    }
}
