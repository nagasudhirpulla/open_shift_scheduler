export async function getShiftTypes(baseAddr) {
    try {        
        const resp = await fetch(`${baseAddr}/api/ShiftTypesApi`, {
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