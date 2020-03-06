import { IShift } from "./IShift";

export interface IShiftParticipation {
    id: number;
    employeeId: string;
    shiftId: number;
    shift?: IShift;
    shiftParticipationTypeId: number;
    participationSequence: number;
}

