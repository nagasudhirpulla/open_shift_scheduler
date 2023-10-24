using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShiftTypesAction
{
    public List<ShiftType> ShiftTypes { get; }
    public SetShiftTypesAction(List<ShiftType> shiftTypes) => ShiftTypes = shiftTypes;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetShiftTypes(ShiftsEditUiState state, SetShiftTypesAction action)
    {
        return state with
        {
            ShiftTypes = action.ShiftTypes
        };
    }
}
