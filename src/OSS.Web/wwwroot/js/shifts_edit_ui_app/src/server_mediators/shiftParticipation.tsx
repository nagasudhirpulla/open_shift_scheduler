import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftGroup } from "../type_defs/IShiftGroup";
import { IShift } from "../type_defs/IShift";

export const createShiftParticipation = async (baseAddr: string, shiftParticipation: IShiftParticipation): Promise<IShiftParticipation> => {
    try {
        let shiftId: number = shiftParticipation.shiftId
        if (shiftId == null) {
            throw new Error("Expected shift id to be non null for shift participation creation")
        }
        // console.log("shift object for shift participation creation is " + JSON.stringify(shift));
        const resp = await fetch(`${baseAddr}/api/ShiftParticipations`, {
            method: 'post',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                "EmployeeId": shiftParticipation.employeeId,
                "ShiftId": shiftId,
                "ShiftParticipationTypeId": shiftParticipation.shiftParticipationTypeId
            })
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON as IShiftParticipation;
    } catch (e) {
        console.log(e);
        //return { success: false, message: `Could not retrieve shifts for shifts ui data due to error ${JSON.stringify(e)}` };
        //return null;
        throw e;
    }
}

export const deleteShiftParticipation = async (baseAddr: string, shiftParticipation: IShiftParticipation): Promise<IShiftParticipation> => {
    try {
        const resp = await fetch(`${baseAddr}/api/ShiftParticipations/${shiftParticipation.id}`, {
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
        return respJSON as IShiftParticipation;
    } catch (e) {
        console.log(e);
        throw e;
    }
}

export const createShiftParticipationsFromShiftGroup = async (baseAddr: string, shiftGroupId: number, shiftId: number): Promise<IShiftParticipation[]> => {
    try {
        // console.log("shift object for shift participation creation is " + JSON.stringify(shift));
        const resp = await fetch(`${baseAddr}/api/ShiftParticipations/FromGroup`, {
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
                "ShiftId": shiftId,
            })
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON as IShiftParticipation[];
    } catch (e) {
        console.log(e);
        throw e;
    }
}

export const editShiftParticipation = async (baseAddr: string, shiftParticipation: IShiftParticipation): Promise<boolean> => {
    try {
        // console.log("shift object for shift participation editing is " + JSON.stringify(ShiftParticipation));
        if (shiftParticipation == undefined || shiftParticipation == null) {
            throw new Error("Invalid shiftParticipation object is being passed as input to the function")
        }
        // edit shift participation in server via api
        const shiftPartId = shiftParticipation.id;
        const resp = await fetch(`${baseAddr}/api/ShiftParticipations/${shiftPartId}`, {
            method: 'PUT',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                "Id": shiftParticipation.id,
                "EmployeeId": shiftParticipation.employeeId,
                "ShiftId": shiftParticipation.shiftId,
                "ShiftParticipationTypeId": shiftParticipation.shiftParticipationTypeId,
                "ParticipationSequence": shiftParticipation.participationSequence
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
        //return { success: false, message: `Could not edit shift participation with id ${shiftPartId}, due to error ${JSON.stringify(e)}` };
        return false;
    }
}

export const moveShiftParticipation = async (baseAddr: string, shiftPartId: number, direction: -1 | 1): Promise<IShift> => {
    try {
        // edit shift participation in server via api
        const resp = await fetch(`${baseAddr}/api/ShiftParticipations/Move`, {
            method: 'POST',
            headers: {
                "accept": "application/json",
                "accept-encoding": "gzip, deflate",
                "accept-language": "en-US,en;q=0.8",
                "content-type": "application/json",
                "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) advanced-rest-client/12.1.3 Chrome/58.0.3029.110 Electron/1.7.12 Safari/537.36"
            },
            body: JSON.stringify({
                ShiftParticipationId: shiftPartId,
                Direction: direction
            })
        });
        // console.log(resp);
        const respJSON = await resp.json();
        return respJSON as IShift;
    } catch (e) {
        console.log(e);
        //return { success: false, message: `Could not edit shift participation with id ${shiftPartId}, due to error ${JSON.stringify(e)}` };
        return null;
    }
}