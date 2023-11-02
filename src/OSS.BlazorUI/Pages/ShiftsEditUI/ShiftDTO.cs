using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI;

public class ShiftDTO : Shift
{
    public new List<ShiftParticipation> ShiftParticipations { get; set; } = new();
}
