export async function getShiftsGroups(baseAddr) {
    try {        
        const resp = await fetch(`${baseAddr}/api/ShiftSkillsApi`, {
            method: 'get'
        });
        const respJSON = await resp.json();
        console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not retrieve shift skills data due to error ${JSON.stringify(e)}` };
    }
}