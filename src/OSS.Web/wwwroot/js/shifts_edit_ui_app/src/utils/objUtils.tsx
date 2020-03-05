import { dateToKeyString } from "./timeUtils";
import { IShiftType } from "../type_defs/IShiftType";
import { IShift } from "../type_defs/IShift";

// https://stackoverflow.com/questions/14446511/most-efficient-method-to-groupby-on-a-array-of-objects
export const groupObjBy = (xs, key) => {
    return xs.reduce(function (rv, x) {
        (rv[x[key]] = rv[x[key]] || []).push(x);
        return rv;
    }, {});
};

export const convertShiftsToMatrix = (startDate: Date, endDate: Date, shifts: IShift[], shiftTypes: IShiftType[]) => {
    // group the shift objects by date and shift type
    let groupedShifts = groupObjBy(shifts, 'shiftDate');
    for (var dateStr in groupedShifts) {
        groupedShifts[dateStr] = groupObjBy(groupedShifts[dateStr], 'shiftTypeId');
    }

    const shiftsMatrix = [];
    for (let dateIter = 0; dateIter <= ((endDate as any) - (startDate as any)) / 86400000; dateIter++) {
        // create row for each shift date
        const shiftDate = new Date(startDate.getTime() + 86400000 * dateIter)
        const dateShifts = [];
        const dateKeyStr = dateToKeyString(shiftDate);
        const dateShiftsObj = groupedShifts[dateKeyStr];

        // create a shift object for each shift type
        for (let shiftTypeIter = 0; shiftTypeIter < shiftTypes.length; shiftTypeIter++) {
            const shiftType = shiftTypes[shiftTypeIter];
            let shiftObj: IShift = {
                shiftType: shiftType,
                shiftTypeId: shiftType.id,
                shiftDate: shiftDate,
                comments: "",
                shiftParticipations: []
            }
            if (dateShiftsObj != undefined) {
                if (dateShiftsObj[shiftType.id] != undefined) {
                    shiftObj = dateShiftsObj[shiftType.id][0];
                }
            }
            dateShifts.push(shiftObj);
        }
        shiftsMatrix.push(dateShifts);
    }
    return shiftsMatrix;
}