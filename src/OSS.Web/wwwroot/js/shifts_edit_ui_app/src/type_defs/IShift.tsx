import { IShiftType } from "./IShiftType";
import { IShiftParticipation } from "./IShiftParticipation";
export interface IShift {
    shiftType: IShiftType;
    shiftTypeId: number;
    shiftDate: Date;
    comments: string;
    shiftParticipations?: IShiftParticipation
}
