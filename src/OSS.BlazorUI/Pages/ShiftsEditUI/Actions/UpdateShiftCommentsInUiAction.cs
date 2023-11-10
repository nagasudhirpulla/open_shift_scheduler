using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class UpdateShiftCommentsInUiAction
{
    public int ShiftId { get; }
    public string Comments { get; }
    public UpdateShiftCommentsInUiAction(int shiftId, string comments)
    {
        ShiftId = shiftId;
        Comments = comments;
    }

    [ReducerMethod]
    public static ShiftsEditUiState OnUpdateShiftCommentsInUi(ShiftsEditUiState state, UpdateShiftCommentsInUiAction action)
    {
        List<ShiftDTO> shifts = state.Shifts;
        bool isShiftFound = false;
        for (var i = 0; i < shifts.Count; i++)
        {
            if (shifts[i].Id == action.ShiftId)
            {
                isShiftFound = true;
                shifts[i].Comments = action.Comments;
                break;
            }
        }
        if (!isShiftFound)
        {
            Console.WriteLine("Shift not found in UI for updating comments");
        }
        return SetShiftsAction.OnSetShifts(state, new SetShiftsAction(shifts));
    }
}
