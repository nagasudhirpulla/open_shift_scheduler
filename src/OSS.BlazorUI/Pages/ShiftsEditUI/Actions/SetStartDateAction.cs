using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetStartDateAction
{
    public DateOnly StartDate { get; }
    public SetStartDateAction(DateOnly startDate) => StartDate = startDate;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetStartDate(ShiftsEditUiState state, SetStartDateAction action)
    {
        return state with
        {
            StartDate = action.StartDate
        };
    }
}
