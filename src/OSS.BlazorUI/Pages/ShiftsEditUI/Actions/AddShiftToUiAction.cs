using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class AddShiftToUiAction
{
    public ShiftDTO Shift { get; }
    public AddShiftToUiAction(ShiftDTO shift) => Shift = shift;

    [ReducerMethod]
    public static ShiftsEditUiState OnAddShift(ShiftsEditUiState state, AddShiftToUiAction action)
    {
        return SetShiftsAction.OnSetShifts(state,
            new SetShiftsAction(state.Shifts.Concat(new[] { action.Shift }).ToList())
            );
    }
}
