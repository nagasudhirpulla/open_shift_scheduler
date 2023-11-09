using Fluxor;
using OSS.Domain.Entities;
using System.Net.Http.Json;


namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class EditShiftParticipationAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public EditShiftParticipationAction(ShiftParticipation shiftParticipation) => ShiftParticipation = shiftParticipation;
}

public class EditShiftParticipationEffect
{
    private readonly HttpClient Http;

    public EditShiftParticipationEffect(HttpClient http)
    {
        Http = http;
    }

    [EffectMethod]
    public async Task EditShiftParticipation(EditShiftParticipationAction action, IDispatcher dispatcher)
    {
        var resp = await Http.PutAsJsonAsync($"api/ShiftParticipations/${action.ShiftParticipation.Id}", action.ShiftParticipation);
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Shift Participation update server request returned error...");
        }
        dispatcher.Dispatch(new UpdateShiftParticipationInUiAction(action.ShiftParticipation));
    }
}
