using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Store;

public static class ShiftsEditUIReducers
{
    [ReducerMethod]
    public static ShiftsEditUiState OnSetShifts(ShiftsEditUiState state, SetShiftsAction action)
    {
        return state with
        {
            Shifts = action.Shifts
        };
    }
}
