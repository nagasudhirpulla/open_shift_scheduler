export async function getShifts(baseAddr) {
    try {        
        const resp = await fetch(`${baseAddr}/api/ShiftsApi`, {
            method: 'get'
        });
        const respJSON = await resp.json();
        console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not retrieve shifts data due to error ${JSON.stringify(e)}` };
    }
}