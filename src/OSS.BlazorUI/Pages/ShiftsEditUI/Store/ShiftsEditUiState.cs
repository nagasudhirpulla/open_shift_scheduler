using Fluxor;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Store;

[FeatureState]
public record ShiftsEditUiState
{
    public DateOnly StartDate { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly EndDate { get; init; } = DateOnly.FromDateTime(DateTime.Now);

    public List<ShiftDTO> Shifts { get; init; } = new();
    public List<ShiftType> ShiftTypes { get; init; } = new();
    public List<ShiftGroup> ShiftGroups { get; init; } = new();

    public List<UserDTO> Employees { get; init; } = new();

    public List<ShiftParticipationType> ShiftParticipationTypes { get; init; } = new();

    public List<ShiftsGridRow> ShiftsInGrid { get; init; } = new();
    public bool ShowAddEmpToShiftModal { get; init; } = false;
    public ShiftDTO ActiveShift { get; init; } = new();

}
