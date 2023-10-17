using OSS.Domain;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI;


public class ShiftsEditUiState
{
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public List<Shift> Shifts { get; set; } = new();
    public List<ShiftType> ShiftTypes { get; set; } = new();

    // TODO use a DTO
    public List<ApplicationUser> Employees { get; set; } = new();

    public List<ShiftParticipationType> ShiftParticipationTypes { get; set; } = new();

    public List<DateShifts> ShiftsByDate { get; set; } = new();
    
}
