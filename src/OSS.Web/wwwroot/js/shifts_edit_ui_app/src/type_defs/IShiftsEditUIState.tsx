import { IShift } from "./IShift";
import { IEmployee } from "./IEmployee";
import { IShiftType } from "./IShiftType";
import { IShiftGroup } from "./IShiftGroup";
import { IShiftParticipationType } from "./IShiftParticipationType";
import { IShiftParticipation } from "./IShiftParticipation";

export interface IShiftsEditUIState {
    ui: {
        shifts: IShift[],
        employees: IEmployee[],
        shiftTypes: IShiftType[],
        shiftGroups: IShiftGroup[],
        shiftParticipationTypes: IShiftParticipationType[],
        startDate: Date,
        endDate: Date,
        activeShift: IShift,
        activeShiftParticipation: IShiftParticipation
    },
    urls: {
        serverBaseAddress: string
    }
}