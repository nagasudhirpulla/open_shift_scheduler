
export async function updateServerShiftParticipation(baseAddr, shiftParticipation) {
    try {
        // console.log("shift object for shift participation editing is " + JSON.stringify(ShiftParticipation));
        if (shiftParticipation == undefined || shiftParticipation == null) {
            throw new Error("Invalid shiftParticipation object is being passed as input to the function")
        }
        // edit shift participation in server via api
        const shiftPartId = shiftParticipation.shiftParticipationId;
        const resp = await fetch(`${baseAddr}/api/ShiftParticipationsApi/${shiftPartId}`, {
            method: 'PUT',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                "ShiftParticipationId": shiftParticipation.shiftParticipationId,
                "EmployeeId": shiftParticipation.employeeId,
                "ShiftId": shiftParticipation.shiftId,
                "ShiftParticipationTypeId": shiftParticipation.shiftParticipationTypeId,
                "ParticipationSequence": shiftParticipation.participationSequence
            })
        });
        // console.log(resp);
        if (resp.status != 204) {
            return { success: false, message: `Status code received was ${resp.status}` };
        }
        return { success: true };
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not edit shift participation with id ${shiftPartId}, due to error ${JSON.stringify(e)}` };
    }
}