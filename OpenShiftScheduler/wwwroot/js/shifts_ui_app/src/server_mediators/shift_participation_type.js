export async function getShiftParticipationTypes(baseAddr) {
    try {        
        const resp = await fetch(`${baseAddr}/api/ShiftParticipationTypesApi`, {
            method: 'get'
        });
        const respJSON = await resp.json();
        //console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not retrieve shift types data due to error ${JSON.stringify(e)}` };
    }
}