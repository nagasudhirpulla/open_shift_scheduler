using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class UpdateShiftParticipationInUiAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public UpdateShiftParticipationInUiAction(ShiftParticipation shiftPart) => ShiftParticipation = shiftPart;

    [ReducerMethod]
    public static ShiftsEditUiState OnUpdateShiftParticipation(ShiftsEditUiState state, UpdateShiftParticipationInUiAction action)
    {
        List<ShiftDTO> shifts = state.Shifts;
        bool isShiftPartFound = false;
        for (var shiftIter = 0; shiftIter < shifts.Count; shiftIter++)
        {
            if (shifts[shiftIter].Id == action.ShiftParticipation.ShiftId)
            {
                for (int partIter = 0; partIter < shifts[shiftIter].ShiftParticipations.Count; partIter++)
                {
                    if (shifts[shiftIter].ShiftParticipations[partIter].Id == action.ShiftParticipation.Id)
                    {
                        shifts[shiftIter].ShiftParticipations[partIter] = action.ShiftParticipation;
                        isShiftPartFound = true;
                        break;
                    }
                }
            }
            if (isShiftPartFound) break;
        }
        if (!isShiftPartFound)
        {
            Console.WriteLine("Shift participation not found in UI for updating");
        }
        return SetShiftsAction.OnSetShifts(state, new SetShiftsAction(shifts));
    }
}
