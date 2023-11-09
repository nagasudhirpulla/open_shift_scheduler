using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetActiveShiftParticipationAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public SetActiveShiftParticipationAction(ShiftParticipation sp) => ShiftParticipation = sp;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetActiveShiftParticipation(ShiftsEditUiState state, SetActiveShiftParticipationAction action)
    {
        return state with
        {
            ActiveShiftParticipation = action.ShiftParticipation
        };
    }
}
