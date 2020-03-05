import { IShift } from "./IShift";
import { IEmployee } from "./IEmployee";
import { IShiftType } from "./IShiftType";
import { IShiftGroup } from "./IShiftGroup";
import { IShiftParticipationType } from "./IShiftParticipationType";

export interface IShiftsEditUIState {
    ui: {
        shifts: IShift[],
        employees: IEmployee[],
        shiftTypes: IShiftType[],
        shiftGroups: IShiftGroup[],
        shiftParticipation_types: IShiftParticipationType[],
        startDate: Date,
        endDate: Date
    },
    urls: {
        serverBaseAddress: string
    }
}