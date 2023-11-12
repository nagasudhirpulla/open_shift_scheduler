using System.Net.Http.Json;
using OSS.Domain.Entities;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.ServerMediators;

public static class DeleteShiftParticipation
{
    public static async Task<ShiftParticipation?> Do(HttpClient Http, ShiftParticipation shiftParticipation)
    {
        HttpResponseMessage resp = await Http.DeleteAsync($"api/ShiftParticipations/{shiftParticipation.Id}");
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Error in deleting a shift participation at server");
            return null;
        }
        ShiftParticipation? deletedShiftPart = await resp.Content.ReadFromJsonAsync<ShiftParticipation>();
        if (deletedShiftPart == null)
        {
            Console.WriteLine("Unable to parse deleted shift object returned from server");
        }
        return deletedShiftPart;
    }
}
