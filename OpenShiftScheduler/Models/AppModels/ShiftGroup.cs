using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenShiftScheduler.Models.AppModels
{
    public class ShiftGroup
    {
        public int ShiftGroupId { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
