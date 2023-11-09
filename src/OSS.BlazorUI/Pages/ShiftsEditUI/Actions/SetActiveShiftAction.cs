using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetActiveShiftAction
{
    public ShiftDTO Shift { get; }
    public SetActiveShiftAction(ShiftDTO shift) => Shift = shift;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetActiveShift(ShiftsEditUiState state, SetActiveShiftAction action)
    {
        return state with
        {
            ActiveShift = action.Shift
        };
    }
}
