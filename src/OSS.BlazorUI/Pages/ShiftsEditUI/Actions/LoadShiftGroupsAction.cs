using System.Net.Http.Json;
using Fluxor;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class LoadShiftGroupsAction
{
}

public class LoadShiftGroupsEffect
{
    private readonly HttpClient Http;

    public LoadShiftGroupsEffect(HttpClient http) => Http = http;

    [EffectMethod(typeof(LoadShiftGroupsAction))]
    public async Task LoadShiftGroups(IDispatcher dispatcher)
    {
        var shiftGroups = await Http.GetFromJsonAsync<List<ShiftGroup>>($"api/ShiftGroups");
        dispatcher.Dispatch(new SetShiftGroupsAction(shiftGroups ?? new()));
    }
}
