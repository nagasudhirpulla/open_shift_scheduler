using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShiftGroupsAction
{
    public List<ShiftGroup> ShiftGroups { get; }
    public SetShiftGroupsAction(List<ShiftGroup> shiftGroups) => ShiftGroups = shiftGroups;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetShiftGroups(ShiftsEditUiState state, SetShiftGroupsAction action)
    {
        return state with
        {
            ShiftGroups = action.ShiftGroups
        };
    }
}
