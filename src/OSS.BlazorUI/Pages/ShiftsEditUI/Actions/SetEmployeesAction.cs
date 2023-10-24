using Fluxor;
using OSS.BlazorUI.Pages.ShiftsEditUI.Store;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class SetEmployeesAction
{
    public List<UserDTO> Employees { get; }
    public SetEmployeesAction(List<UserDTO> employees) => Employees = employees;

    [ReducerMethod]
    public static ShiftsEditUiState OnSetEmployees(ShiftsEditUiState state, SetEmployeesAction action)
    {
        return state with
        {
            Employees = action.Employees
        };
    }
}
