import { IShiftGroup } from "../type_defs/IShiftGroup";

export const getShiftGroups = async (baseAddr: string): Promise<IShiftGroup[]> => {
    try {
        const resp = await fetch(`${baseAddr}/api/ShiftGroups`, {
            method: 'get'
        });
        const respJSON = await resp.json() as IShiftGroup[];
        //console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.error(e);
        return [];
        //return { success: false, message: `Could not retrieve employees data due to error ${JSON.stringify(e)}` };
    }
};