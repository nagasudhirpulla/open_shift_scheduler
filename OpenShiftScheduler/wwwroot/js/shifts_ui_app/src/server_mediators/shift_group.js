export async function getShiftsGroups(baseAddr) {
    try {        
        const resp = await fetch(`${baseAddr}/api/ShiftGroupsApi`, {
            method: 'get'
        });
        const respJSON = await resp.json();
        // console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not retrieve shift groups data due to error ${e.toString()}` };
    }
}