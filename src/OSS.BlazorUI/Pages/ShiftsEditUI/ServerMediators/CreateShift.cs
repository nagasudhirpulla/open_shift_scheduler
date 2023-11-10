using System.Net.Http.Json;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.ServerMediators;

public static class CreateShift
{
    public static async Task<ShiftDTO?> Do(HttpClient Http, ShiftDTO shift)
    {
        // do this to avoid error of creating a duplicate shift type
        shift.ShiftType = null;
        HttpResponseMessage resp = await Http.PostAsJsonAsync($"api/Shifts", shift);
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Error in creating a shift for creating shift participation");
            return null;
        }
        ShiftDTO? createdShift = await resp.Content.ReadFromJsonAsync<ShiftDTO>();
        return createdShift;
    }
}
