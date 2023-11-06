using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class AddShiftParticipationToUiAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public AddShiftParticipationToUiAction(ShiftParticipation shiftParticipation) => ShiftParticipation = shiftParticipation;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetShiftParticipationTypes(ShiftsEditUiState state, AddShiftParticipationToUiAction action)
    {
        // get the index of the required shift
        int shiftInd = state.Shifts.FindIndex(s => s.Id == action.ShiftParticipation.ShiftId);
        if (shiftInd == -1)
        {
            Console.WriteLine($"Shift not found in UI to add new shift participation {action.ShiftParticipation}");
            return state;
        }
        var shifts = state.Shifts;
        shifts[shiftInd].ShiftParticipations.Add(action.ShiftParticipation);

        return SetShiftsAction.OnSetShifts(state, new SetShiftsAction(shifts));
    }
}
