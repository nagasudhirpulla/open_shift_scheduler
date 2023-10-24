using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShiftParticipationTypesAction
{
    public List<ShiftParticipationType> ShiftParticipationTypes { get; }
    public SetShiftParticipationTypesAction(List<ShiftParticipationType> shiftParticipationTypes) => ShiftParticipationTypes = shiftParticipationTypes;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetShiftParticipationTypes(ShiftsEditUiState state, SetShiftParticipationTypesAction action)
    {
        return state with
        {
            ShiftParticipationTypes = action.ShiftParticipationTypes
        };
    }
}
