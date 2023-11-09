using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class UpdateShiftInUiAction
{
    public ShiftDTO Shift { get; }
    public UpdateShiftInUiAction(ShiftDTO shift) => Shift = shift;

    [ReducerMethod]
    public static ShiftsEditUiState OnUpdateShiftInUi(ShiftsEditUiState state, UpdateShiftInUiAction action)
    {
        List<ShiftDTO> shifts = state.Shifts;
        bool isShiftFound = false;
        for (var i = 0; i < shifts.Count; i++)
        {
            if (shifts[i].Id == action.Shift.Id)
            {
                isShiftFound = true;
                shifts[i] = action.Shift;
                break;
            }
        }
        if (!isShiftFound)
        {
            Console.WriteLine("Shift not found in UI for updating");
        }
        return SetShiftsAction.OnSetShifts(state, new SetShiftsAction(shifts));
    }
}
