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
        ShiftParticipation? deletedShiftPart = await ServerMediators.DeleteShiftParticipation.Do(Http, action.ShiftParticipation);
        if (deletedShiftPart == null)
        {
            return;
        }
        // delete shift participation from UI
        dispatcher.Dispatch(new DeleteShiftParticipationFromUiAction(deletedShiftPart));
    }
}
