using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetShowEditShiftCommentsModalAction
{
    public bool IsVisible { get; }
    public SetShowEditShiftCommentsModalAction(bool isVisible) => IsVisible = isVisible;

    [ReducerMethod]
    public static ShiftsEditUiState OnShowEditShiftCommentsModal(ShiftsEditUiState state, SetShowEditShiftCommentsModalAction action)
    {
        return state with
        {
            EditShiftCommentsModalState = state.EditShiftCommentsModalState with
            {
                showModal = action.IsVisible,
                Comments = state.ActiveShift.Comments
            }
        };
    }
}
