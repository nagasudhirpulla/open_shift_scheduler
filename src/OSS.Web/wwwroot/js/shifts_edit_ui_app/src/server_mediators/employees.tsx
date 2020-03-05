import { IEmployee } from "../type_defs/IEmployee";

export const getEmployees = async (baseAddr: string): Promise<IEmployee[]> => {
    try {
        const resp = await fetch(`${baseAddr}/api/Employees`, {
            method: 'get'
        });
        const respJSON = await resp.json() as IEmployee[];
        //console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.error(e);
        return [];
        //return { success: false, message: `Could not retrieve employees data due to error ${JSON.stringify(e)}` };
    }
};