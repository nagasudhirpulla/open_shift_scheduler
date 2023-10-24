using System.Net.Http.Json;
using Fluxor;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class LoadShiftParticipationTypesAction
{
}

public class LoadShiftParticipationTypesEffect
{
    private readonly HttpClient Http;

    public LoadShiftParticipationTypesEffect(HttpClient http) => Http = http;

    [EffectMethod(typeof(LoadShiftTypesAction))]
    public async Task LoadShiftParticipationTypes(IDispatcher dispatcher)
    {
        // TODO handle deserialization explicitly
        var ShiftParticipationTypes = await Http.GetFromJsonAsync<List<ShiftParticipationType>>($"api/ShiftParticipationTypes");
        dispatcher.Dispatch(new SetShiftParticipationTypesAction(ShiftParticipationTypes ?? new()));
    }
}
