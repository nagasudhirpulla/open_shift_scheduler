import { IShiftParticipationType } from "../type_defs/IShiftParticipationType";

export const getShiftParticipationTypes = async (baseAddr: string): Promise<IShiftParticipationType[]> => {
    try {
        const resp = await fetch(`${baseAddr}/api/ShiftParticipationTypes`, {
            method: 'get'
        });
        const respJSON = await resp.json() as IShiftParticipationType[];
        //console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.error(e);
        return [];
        //return { success: false, message: `Could not retrieve employees data due to error ${JSON.stringify(e)}` };
    }
};