using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShowAddShiftEmpModalAction
{
    public bool IsVisible { get; }
    public SetShowAddShiftEmpModalAction(bool isVisible) => IsVisible = isVisible;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetStartDate(ShiftsEditUiState state, SetShowAddShiftEmpModalAction action)
    {
        return state with
        {
            ShowAddEmpToShiftModal = action.IsVisible
        };
    }
}
