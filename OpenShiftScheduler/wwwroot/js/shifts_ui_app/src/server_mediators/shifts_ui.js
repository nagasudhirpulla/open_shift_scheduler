
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

export async function createShift(baseAddr, shift) {
    try {
        // console.log("shift object for shift creation is " + JSON.stringify(shift));
        //create shift in server via api, and return the created shift object
        const resp = await fetch(`${baseAddr}/api/ShiftsApi`, {
            method: 'post',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                "shiftTypeId": shift.shiftTypeId,
                "shiftDate": shift.shiftDate
            })
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not create shift, due to error ${JSON.stringify(e)}` };
    }
}

export async function createServerShiftParticipation(baseAddr, employeeId, shiftParticipationTypeId, shift) {
    try {
        // console.log("shift object for shift participation creation is " + JSON.stringify(shift));
        if (shift.shiftId == null) {
            return { success: false, message: `could not create shift participation for shift since shift id is null` };
        }
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
                "ShiftId": shift.shiftId,
                "ShiftParticipationTypeId": shiftParticipationTypeId
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

export async function addShiftParticipationFromShiftGroup(baseAddr, shiftGroupId, shift) {
    try {
        // console.log("shift object for shift participation creation is " + JSON.stringify(shift));
        if (shift.shiftId == null) {
            return { success: false, message: `could not create shift participation for shift since shift id is null` };
        }
        const resp = await fetch(`${baseAddr}/api/ShiftParticipationsApi/FromGroup`, {
            method: 'post',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                "ShiftGroupId": shiftGroupId,
                "ShiftId": shift.shiftId,
                "ShiftParticipationTypeId": null
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

export async function deleteShiftParticipation(baseAddr, shiftParticipation) {
    try {
        if (shiftParticipation.shiftParticipationId == null) {
            return { success: false, message: `could not delete shift participation since id is null` };
        }
        const resp = await fetch(`${baseAddr}/api/ShiftParticipationsApi/${shiftParticipation.shiftParticipationId}`, {
            method: 'delete',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            }
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not delete shift participation due to error ${JSON.stringify(e)}` };
    }
}

export async function deleteShift(baseAddr, shift) {
    try {
        if (shift.shiftId == null) {
            return { success: false, message: `could not delete shift since id is null` };
        }
        const resp = await fetch(`${baseAddr}/api/ShiftsApi/${shift.shiftId}`, {
            method: 'delete',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            }
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not delete shift due to error ${JSON.stringify(e)}` };
    }
}

export async function moveServerShiftParticipation(baseAddr, shiftParticipation, direction) {
    try {
        if (shiftParticipation.shiftParticipationId == null) {
            return { success: false, message: `could not move shift participation since id is null` };
        }
        const resp = await fetch(`${baseAddr}/api/ShiftsUI/MoveShiftParticipation/${shiftParticipation.shiftParticipationId}`, {
            method: 'POST',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({ "direction": direction })
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON;
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not move shift participation ${direction == 1 ? "up" : "down"}, due to error ${JSON.stringify(e)}` };
    }
}

export async function updateServerShiftComments(baseAddr, shift) {
    try {
        // console.log("shift object for shift creation is " + JSON.stringify(shift));
        if (shift == undefined || shift == null) {
            throw new Error("Invalid shift object is being passed as input to the function")
        }
        let comments = "";
        if (shift.comments != null) {
            comments = shift.comments;
        }
        //create shift in server via api, and return the created shift object
        const resp = await fetch(`${baseAddr}/api/ShiftsUI/PutShiftComments/${shift.shiftId}`, {
            method: 'PUT',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({ "comments": comments })
        });
        // console.log(resp);
        if (resp.status != 204) {
            return { success: false, message: `Status code received was ${resp.status}` };
        }
        return { success: true };
    } catch (e) {
        console.log(e);
        return { success: false, message: `Could not create shift, due to error ${JSON.stringify(e)}` };
    }
}