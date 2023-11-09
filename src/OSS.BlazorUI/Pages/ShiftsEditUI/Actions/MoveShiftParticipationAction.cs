using Fluxor;
using System.Net.Http.Json;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class MoveShiftParticipationAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public bool IsUp { get; }
    public MoveShiftParticipationAction(ShiftParticipation shiftParticipation, bool isUp)
    {
        ShiftParticipation = shiftParticipation;
        IsUp = isUp;
    }
}

public class MoveShiftParticipationEffect
{
    private readonly HttpClient Http;

    public MoveShiftParticipationEffect(HttpClient http)
    {
        Http = http;
    }

    [EffectMethod]
    public async Task MoveShiftParticipation(MoveShiftParticipationAction action, IDispatcher dispatcher)
    {
        HttpResponseMessage resp = await Http.PostAsJsonAsync($"api/ShiftParticipations/Move", new { Direction = action.IsUp ? -1 : 1, ShiftParticipationId = action.ShiftParticipation.Id });
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Error returned by Move shift participation server action");
        }
        ShiftDTO? updatedShift = await resp.Content.ReadFromJsonAsync<ShiftDTO>();
        if (updatedShift == null)
        {
            Console.WriteLine("Response not found in desired format after moving shift participation at server");
            return;
        }
        // update the modified shift in UI
        dispatcher.Dispatch(new UpdateShiftInUiAction(updatedShift));
    }
}
