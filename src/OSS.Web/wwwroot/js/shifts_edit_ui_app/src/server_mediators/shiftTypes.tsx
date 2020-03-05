import { IShiftType } from "../type_defs/IShiftType";

export const getShiftTypes = async (baseAddr: string): Promise<IShiftType[]> => {
    try {
        const resp = await fetch(`${baseAddr}/api/ShiftTypes`, {
            method: 'get'
        });
        const respJSON = await resp.json() as IShiftType[];
        //console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.error(e);
        return [];
        //return { success: false, message: `Could not retrieve employees data due to error ${JSON.stringify(e)}` };
    }
};