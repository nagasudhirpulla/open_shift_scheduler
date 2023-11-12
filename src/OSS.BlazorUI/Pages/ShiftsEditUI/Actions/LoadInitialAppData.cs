using Fluxor;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class LoadInitialAppDataAction
{
    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }
    public LoadInitialAppDataAction(DateOnly startDt, DateOnly endDt)
    {
        StartDate = startDt;
        EndDate = endDt;
    }
}

public class LoadInitialAppDataEffect
{
    private readonly HttpClient Http;

    public LoadInitialAppDataEffect(HttpClient http) => Http = http;

    [EffectMethod]
    public async Task LoadInitialAppData(LoadInitialAppDataAction action, IDispatcher dispatcher)
    {
        await new LoadEmployeesEffect(Http).LoadEmployees(dispatcher);
        await new LoadShiftTypesEffect(Http).LoadShiftTypes(dispatcher);
        await new LoadShiftParticipationTypesEffect(Http).LoadShiftParticipationTypes(dispatcher);
        await new LoadShiftGroupsEffect(Http).LoadShiftGroups(dispatcher);
        await new LoadShiftsEffect(Http).LoadShifts(new LoadShiftsAction(action.StartDate, action.EndDate), dispatcher);
    }
}
