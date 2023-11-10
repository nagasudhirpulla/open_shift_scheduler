using Fluxor;
using OSS.Domain.Entities;
using System.Net.Http.Json;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class CreateShiftParticipationFromGroupAction
{
    public int ShiftId { get; }
    public int ShiftGroupId { get; }
    public CreateShiftParticipationFromGroupAction(int sId, int sgId)
    {
        ShiftId = sId;
        ShiftGroupId = sgId;
    }
}

public class CreateShiftParticipationFromGroupEffect
{
    private readonly HttpClient Http;

    public CreateShiftParticipationFromGroupEffect(HttpClient http) => Http = http;

    [EffectMethod]
    public async Task CreateShiftParticipationFromGroup(CreateShiftParticipationFromGroupAction action, IDispatcher dispatcher)
    {
        HttpResponseMessage resp = await Http.PostAsJsonAsync($"api/ShiftParticipations/FromGroup", action);
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Error in creating shift participations from shift group at server");
            return;
        }
        List<ShiftParticipation>? createdShiftParticipations = await resp.Content.ReadFromJsonAsync<List<ShiftParticipation>>();
        if (createdShiftParticipations == null)
        {
            Console.WriteLine("Unable to parse created shift participations list returned from server");
            return;
        }

        foreach (var sp in createdShiftParticipations)
        {
            dispatcher.Dispatch(new AddShiftParticipationToUiAction(sp));
        }
        // TODO test this
    }
}
