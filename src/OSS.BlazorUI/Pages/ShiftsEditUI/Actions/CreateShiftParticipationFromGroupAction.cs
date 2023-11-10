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
        ShiftDTO shift = action.Shift;
        if (shift.Id <= 0)
        {
            // create shift if not present at server
            var createdShift = await ServerMediators.CreateShift.Do(Http, action.Shift);
            if (createdShift == null)
            {
                Console.WriteLine("Unable to parse created shift object returned from server");
                return;
            }
            shift = createdShift;
            // add created shift to UI
            dispatcher.Dispatch(new AddShiftToUiAction(createdShift));
        }
        // create shift participations at server
        HttpResponseMessage resp = await Http.PostAsJsonAsync($"api/ShiftParticipations/FromGroup", new { ShiftId = shift.Id, ShiftGroupId = action.ShiftGroupId });
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Error in creating shift participations from shift group at server");
            return;
        }
        List<ShiftParticipation>? updatedShiftParticipations = await resp.Content.ReadFromJsonAsync<List<ShiftParticipation>>();
        if (updatedShiftParticipations == null)
        {
            Console.WriteLine("Unable to parse created shift participations list returned from server");
            return;
        }

        // add created shift participations to UI
        foreach (var sp in updatedShiftParticipations)
        {
            if (!shift.ShiftParticipations.Any(x => x.Id == sp.Id))
            {
                // add the shift participation to UI if not present
                dispatcher.Dispatch(new AddShiftParticipationToUiAction(sp));
            }
        }
    }
}
