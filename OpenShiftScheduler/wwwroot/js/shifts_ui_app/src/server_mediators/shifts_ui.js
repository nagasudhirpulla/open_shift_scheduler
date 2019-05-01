
import { dateToApiStr } from '../utils/timeUtils';

export async function getShiftsForUI(baseAddr, start_date, end_date) {
    try {
        const start_date_str = dateToApiStr(start_date);
        const end_date_str = dateToApiStr(end_date);
        const resp = await fetch(`${baseAddr}/api/ShiftsUI/shifts?start_date=${start_date_str}&end_date=${end_date_str}`, {
            method: 'get'
        });
        //console.log(resp);
        const respJSON = await resp.json();
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not retrieve shifts for shifts ui data due to error ${JSON.stringify(e)}` };
    }
}

export async function createShiftParticipation(baseAddr, employeeId, shift) {
    try {
        // console.log("shift object for shift participation cretion is " + JSON.stringify(shift));
        //todo create shift in server via api if shft id is null, create shift in server, update shift object in redux state, and the add shift participaion
        const resp = await fetch(`${baseAddr}/api/ShiftParticipationsApi`, {
            method: 'post',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                "EmployeeId": employeeId,
                "ShiftId": shift.shiftId
            })
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not retrieve shifts for shifts ui data due to error ${JSON.stringify(e)}` };
    }
}