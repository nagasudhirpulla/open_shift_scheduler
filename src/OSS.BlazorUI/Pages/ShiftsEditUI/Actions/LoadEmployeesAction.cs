using System.Net.Http.Json;
using Fluxor;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class LoadEmployeesAction
{
}

public class LoadEmployeesEffect
{
    private readonly HttpClient Http;

    public LoadEmployeesEffect(HttpClient http) => Http = http;

    [EffectMethod(typeof(LoadEmployeesAction))]
    public async Task LoadEmployees(IDispatcher dispatcher)
    {
        var employees = await Http.GetFromJsonAsync<List<UserDTO>>($"api/Employees");
        dispatcher.Dispatch(new SetEmployeesAction(employees ?? new()));
    }
}
