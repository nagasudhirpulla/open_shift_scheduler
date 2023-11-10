using Fluxor;
using System.Net.Http.Json;

namespace OSS.BlazorUI.Pages.ShiftsEditUI.Actions;

public class EditShiftCommentsAction
{
    public string Comments { get; }
    public ShiftDTO Shift { get; }
    public EditShiftCommentsAction(ShiftDTO s, string comments)
    {
        Shift = s;
        Comments = comments;
    }
}

public class EditShiftCommentsEffect
{
    private readonly HttpClient Http;

    public EditShiftCommentsEffect(HttpClient http) => Http = http;

    [EffectMethod]
    public async Task EditShiftComments(EditShiftCommentsAction action, IDispatcher dispatcher)
    {
        ShiftDTO shift = action.Shift;
        if (shift.Id <= 0)
        {
            // create shift if not present at server
            var createdShift = await ServerMediators.CreateShift.Do(Http, action.Shift);
            if (createdShift == null)
            {
                Console.WriteLine("Unable to parse created shift object returned from server");
                return;
            }
            shift = createdShift;
            // add created shift to UI
            dispatcher.Dispatch(new AddShiftToUiAction(createdShift));
        }
        // edit shift comments at server
        HttpResponseMessage resp = await Http.PutAsJsonAsync($"api/Shifts/Comments", new { ShiftId = shift.Id, Comments = action.Comments });
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Error while editing shift comments at server");
            return;
        }
        // update shift comments in UI
        dispatcher.Dispatch(new UpdateShiftCommentsInUiAction(shift.Id, action.Comments));
    }
}

