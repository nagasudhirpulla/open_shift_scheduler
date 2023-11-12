using Fluxor;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class DeleteAllShiftParticipationsAction
{
    public ShiftDTO Shift { get; }
    public DeleteAllShiftParticipationsAction(ShiftDTO s)
    {
        Shift = s;
    }
}

public class DeleteAllShiftParticipationsEffect
{
    private readonly HttpClient Http;

    public DeleteAllShiftParticipationsEffect(HttpClient http) => Http = http;

    [EffectMethod]
    public async Task DeleteAllShiftParticipations(DeleteAllShiftParticipationsAction action, IDispatcher dispatcher)
    {
        var deletedParts = new List<ShiftParticipation>();
        foreach (var sp in action.Shift.ShiftParticipations)
        {
            ShiftParticipation? deletedShiftPart = await ServerMediators.DeleteShiftParticipation.Do(Http, sp);
            if (deletedShiftPart == null)
            {
                continue;
            }
            deletedParts.Add(deletedShiftPart);
        }
        // delete shift participation from UI
        foreach (var sp in deletedParts)
        {
            dispatcher.Dispatch(new DeleteShiftParticipationFromUiAction(sp));
        }
    }
}
