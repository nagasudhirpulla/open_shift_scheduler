import { IShift } from "../type_defs/IShift";
import { dateToApiStr, dateTimeToApiStr } from "../utils/timeUtils";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

// using postman to test apis - https://www.youtube.com/watch?v=u9iBjM-x5Jc
export const getShiftsBetweenDates = async (baseAddr: string, startDate: Date, endDate: Date): Promise<IShift[]> => {
    try {
        const start_date_str = dateToApiStr(startDate);
        const end_date_str = dateToApiStr(endDate);
        const resp = await fetch(`${baseAddr}/api/Shifts/BetweenDates?start_date=${start_date_str}&end_date=${end_date_str}`, {
            method: 'get'
        });
        const respJSON = await resp.json() as IShift[];
        //console.log(respJSON);
        return respJSON;
    } catch (e) {
        console.error(e);
        return [];
        //return { success: false, message: `Could not retrieve employees data due to error ${JSON.stringify(e)}` };
    }
};

export const createShift = async (baseAddr: string, shift: IShift): Promise<IShift> => {
    try {
        // console.log("shift object for shift creation is " + JSON.stringify(shift));
        //create shift in server via api, and return the created shift object
        const resp = await fetch(`${baseAddr}/api/Shifts`, {
            method: 'post',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                ShiftTypeId: shift.shiftTypeId,
                ShiftDate: dateTimeToApiStr(shift.shiftDate)
            })
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON as IShift;
    } catch (e) {
        console.log(e);
        //return { success: false, message: `Could not create shift, due to error ${JSON.stringify(e)}` };
        //return null;
        throw e;
    }
}

export const editShiftComment = async (baseAddr: string, comment: string, shift: IShift): Promise<boolean> => {
    try {
        // console.log("shift object for shift creation is " + JSON.stringify(shift));
        const resp = await fetch(`${baseAddr}/api/Shifts/Comments`, {
            method: 'put',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                ShiftId: shift.id,
                Comments: comment
            })
        });
        // console.log(resp);
        if (resp.status != 204) {
            //return { success: false, message: `Status code received was ${resp.status}` };
            return false;
        }
        return true;
    } catch (e) {
        console.log(e);
        //return { success: false, message: `Could not create shift, due to error ${JSON.stringify(e)}` };
        //return null;
        throw e;
    }
}