export async function getEmployees(baseAddr) {
    try {        
        const resp = await fetch(`${baseAddr}/api/EmloyeesApi`, {
            method: 'get'
        });
        const respJSON = await resp.json();
        console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not retrieve employees data due to error ${JSON.stringify(e)}` };
    }
}