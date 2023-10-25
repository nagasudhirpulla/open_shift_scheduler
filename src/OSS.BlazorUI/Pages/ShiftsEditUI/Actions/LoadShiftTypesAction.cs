using System.Net.Http.Json;
using Fluxor;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class LoadShiftTypesAction
{
}

public class LoadShiftTypesEffect
{
    private readonly HttpClient Http;

    public LoadShiftTypesEffect(HttpClient http) => Http = http;

    [EffectMethod(typeof(LoadShiftTypesAction))]
    public async Task LoadShiftTypes(IDispatcher dispatcher)
    {
        List<ShiftType> shiftTypes = (await Http.GetFromJsonAsync<List<ShiftType>>($"api/ShiftTypes") ?? new()).OrderBy(st => st.ShiftSequence).ToList();
        dispatcher.Dispatch(new SetShiftTypesAction(shiftTypes));
    }
}
