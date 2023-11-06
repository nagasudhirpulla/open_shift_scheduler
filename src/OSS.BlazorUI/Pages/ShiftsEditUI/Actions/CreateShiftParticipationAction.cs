using System.Net.Http.Json;
using Fluxor;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class CreateShiftParticipationAction
{
    public ShiftParticipation ShiftParticipation { get; }
    public CreateShiftParticipationAction(ShiftParticipation sp) => ShiftParticipation = sp;
}

public class CreateShiftParticipationEffect
{
    private readonly HttpClient Http;

    public CreateShiftParticipationEffect(HttpClient http) => Http = http;

    [EffectMethod]
    public async Task LoadShiftTypes(CreateShiftParticipationAction action, IDispatcher dispatcher)
    {
        var shiftParticipation = action.ShiftParticipation;
        if (shiftParticipation.ShiftId <= 0)
        {
            // create shift if not present
            var resp = await Http.PostAsJsonAsync($"api/Shifts", shiftParticipation.Shift);
            if (!resp.IsSuccessStatusCode)
            {
                Console.WriteLine("Error in creating shift with participation");
                //TODO print response body also
                return;
            }
            Shift createdShift = await resp.Content.ReadFromJsonAsync<Shift>() ?? new Shift();
            // TODO add the new shift to UI
            shiftParticipation.ShiftId = createdShift.Id;
        }
        var response = await Http.PostAsJsonAsync($"api/ShiftParticipations", shiftParticipation);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Error in creating shift participation");
            //TODO print response body also
            return;
        }
        
        var createdSp = await response.Content.ReadFromJsonAsync<ShiftParticipation>();
        // TODO add created shift to UI
    }
}
