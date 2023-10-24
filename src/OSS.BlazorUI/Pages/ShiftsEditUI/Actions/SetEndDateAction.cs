using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetEndDateAction
{
    public DateOnly EndDate { get; }
    public SetEndDateAction(DateOnly endDate) => EndDate = endDate;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetEndDate(ShiftsEditUiState state, SetEndDateAction action)
    {
        return state with
        {
            EndDate = action.EndDate
        };
    }
}
