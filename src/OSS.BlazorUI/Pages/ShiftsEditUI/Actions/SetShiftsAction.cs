using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShiftsAction
{
    public List<ShiftDTO> Shifts { get; }
    public SetShiftsAction(List<ShiftDTO> shifts) => Shifts = shifts;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetShifts(ShiftsEditUiState state, SetShiftsAction action)
    {
        var shiftsInTable = ShiftsGridRow.FromShifts(action.Shifts, state.ShiftTypes, state.StartDate, state.EndDate);
        return state with
        {
            Shifts = action.Shifts,
            ShiftsInGrid = shiftsInTable,
        };
    }
}
