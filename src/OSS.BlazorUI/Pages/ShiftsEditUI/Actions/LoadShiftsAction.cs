using Fluxor;
using System.Net.Http.Json;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class LoadShiftsAction
{
    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }
    public LoadShiftsAction(DateOnly startDate, DateOnly endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}


public class LoadShiftsEffect
{
    private readonly HttpClient Http;

    public LoadShiftsEffect(HttpClient http)
    {
        Http = http;
    }

    [EffectMethod]
    public async Task LoadShifts(LoadShiftsAction action, IDispatcher dispatcher)
    {
        var shifts = await Http.GetFromJsonAsync<List<ShiftDTO>>($"api/Shifts/BetweenDates?start_date={action.StartDate:yyyy-MM-dd}&end_date={action.EndDate:yyyy-MM-dd}");
        dispatcher.Dispatch(new SetShiftsAction(shifts ?? new()));
    }
}
