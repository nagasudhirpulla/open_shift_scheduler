using Fluxor;
using OSS.Domain.Entities;
using System.Net.Http.Json;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class CreateShiftParticipationFromGroupAction
{
    public ShiftDTO Shift { get; }
    public int ShiftGroupId { get; }
    public CreateShiftParticipationFromGroupAction(ShiftDTO shift, int sgId)
    {
        Shift = shift;
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
        // create shift if not present at server and add to UI
        int shiftId = action.Shift.Id;
        if (shiftId <= 0)
        {
            var createdShift = await ServerMediators.CreateShift.Do(Http, action.Shift);
            if (createdShift == null)
            {
                Console.WriteLine("Unable to parse created shift object returned from server");
                return;
            }
            shiftId = createdShift.Id;
            dispatcher.Dispatch(new AddShiftToUiAction(createdShift));
        }
        // create shift participations at server and add to UI
        HttpResponseMessage resp = await Http.PostAsJsonAsync($"api/ShiftParticipations/FromGroup", new { ShiftId = shiftId, ShiftGroupId = action.ShiftGroupId });
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
