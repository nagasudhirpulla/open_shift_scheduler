import { IShiftType } from "./IShiftType";
import { IShiftParticipation } from "./IShiftParticipation";
export interface IShift {
    id: number;
    shiftType: IShiftType;
    shiftTypeId: number;
    shiftDate: Date;
    comments: string;
    shiftParticipations?: IShiftParticipation[]
}
