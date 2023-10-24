using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShiftsAction
{
    public List<Shift> Shifts { get; }
    public SetShiftsAction(List<Shift> shifts) => Shifts = shifts;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetShifts(ShiftsEditUiState state, SetShiftsAction action)
    {
        var shiftsInTable = DateShifts.FromShifts(state.Shifts);
        return state with
        {
            Shifts = action.Shifts,
            ShiftsInTable = shiftsInTable,
        };
    }
}
