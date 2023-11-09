using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShowEditShiftParticipationModalAction
{
    public bool IsVisible { get; }
    public SetShowEditShiftParticipationModalAction(bool isVisible) => IsVisible = isVisible;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetShowEditShiftParticipationModal(ShiftsEditUiState state, SetShowEditShiftParticipationModalAction action)
    {
        return state with
        {
            ShowEditShiftParticipationModal = action.IsVisible
        };
    }
}
