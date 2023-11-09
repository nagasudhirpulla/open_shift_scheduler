using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class DeleteShiftParticipationFromUiAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public DeleteShiftParticipationFromUiAction(ShiftParticipation shiftParticipation) => ShiftParticipation = shiftParticipation;

    [ReducerMethod]
    public static ShiftsEditUiState OnDeleteShiftParticipationFromUi(ShiftsEditUiState state, DeleteShiftParticipationFromUiAction action)
    {
        int shiftInd = -1;
        int shiftPartInd = -1;

        // find the shift and shift participation that contains this participation
        for (int sIter = 0; sIter < state.Shifts.Count; sIter++)
        {
            var shift = state.Shifts[sIter];
            for (int spIter = 0; spIter < shift.ShiftParticipations.Count; spIter++)
            {
                var sp = shift.ShiftParticipations[spIter];
                if (sp.Id == action.ShiftParticipation.Id)
                {
                    shiftInd = sIter;
                    shiftPartInd = spIter;
                    break;
                }
            }
            if (shiftInd != -1) break;
        }

        if (shiftInd == -1)
        {
            Console.WriteLine($"Shift participation not found in UI for deletion");
            return state;
        }

        var shifts = state.Shifts;
        shifts[shiftInd].ShiftParticipations.RemoveAt(shiftPartInd);

        return SetShiftsAction.OnSetShifts(state, new SetShiftsAction(shifts));
    }
}
