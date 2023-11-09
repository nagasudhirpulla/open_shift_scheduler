using Fluxor;
using System.Net.Http.Json;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class DeleteShiftParticipationAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public DeleteShiftParticipationAction(ShiftParticipation sp)
    {
        ShiftParticipation = sp;
    }
}

public class DeleteShiftParticipationEffect
{
    private readonly HttpClient Http;

    public DeleteShiftParticipationEffect(HttpClient http) => Http = http;

    [EffectMethod]
    public async Task DeleteShiftParticipation(DeleteShiftParticipationAction action, IDispatcher dispatcher)
    {
        var shiftParticipation = action.ShiftParticipation;
        HttpResponseMessage resp = await Http.DeleteAsync($"api/ShiftParticipations/{shiftParticipation.Id}");
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Error in deleting a shift participation at server");
            return;
        }
        ShiftParticipation? deletedShiftPart = await resp.Content.ReadFromJsonAsync<ShiftParticipation>();
        if (deletedShiftPart == null)
        {
            Console.WriteLine("Unable to parse deleted shift object returned from server");
            return;
        }
        // delete shift participation from UI
        dispatcher.Dispatch(new DeleteShiftParticipationFromUiAction(deletedShiftPart));
    }
}
