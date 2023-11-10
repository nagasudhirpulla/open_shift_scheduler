using System.Net.Http.Json;
using Fluxor;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class CreateShiftParticipationAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public ShiftDTO Shift { get; }
    public CreateShiftParticipationAction(ShiftParticipation sp, ShiftDTO shift)
    {
        ShiftParticipation = sp;
        Shift = shift;
    }
}

public class CreateShiftParticipationEffect
{
    private readonly HttpClient Http;

    public CreateShiftParticipationEffect(HttpClient http) => Http = http;

    [EffectMethod]
    public async Task CreateShiftParticipation(CreateShiftParticipationAction action, IDispatcher dispatcher)
    {
        var shiftParticipation = action.ShiftParticipation;
        if (shiftParticipation.ShiftId <= 0)
        {
            // create shift if not present
            var shift = action.Shift;
            var createdShift = await ServerMediators.CreateShift.Do(Http, shift);
            if (createdShift == null)
            {
                Console.WriteLine("Unable to parse created shift object returned from server");
                return;
            }
            shiftParticipation.ShiftId = createdShift.Id;
            // Add the newly created shift to UI
            dispatcher.Dispatch(new AddShiftToUiAction(createdShift));
        }
        // add shift participation to shift at server
        var response = await Http.PostAsJsonAsync($"api/ShiftParticipations", shiftParticipation);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Error in creating shift participation");
            return;
        }

        var createdSp = await response.Content.ReadFromJsonAsync<ShiftParticipation>();
        if (createdSp == null)
        {
            Console.WriteLine("Unable to parse created shift participation object returned from server");
            return;
        }
        // add newly created shift participation to UI
        dispatcher.Dispatch(new AddShiftParticipationToUiAction(createdSp));
    }
}
