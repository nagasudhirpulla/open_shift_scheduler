using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShowAddParticipationsFromGrpModalAction
{
    public bool IsVisible { get; }
    public SetShowAddParticipationsFromGrpModalAction(bool isVisible) => IsVisible = isVisible;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetShowAddParticipationsFromGrpModal(ShiftsEditUiState state, SetShowAddParticipationsFromGrpModalAction action)
    {
        return state with
        {
            ShowAddParticipationsFromGrpModal = action.IsVisible
        };
    }
}
