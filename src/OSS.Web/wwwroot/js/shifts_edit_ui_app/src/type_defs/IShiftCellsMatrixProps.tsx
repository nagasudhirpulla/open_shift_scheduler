import { IShift } from "./IShift";
import { IShiftType } from "./IShiftType";
import { IEmployee } from "./IEmployee";
import { IShiftParticipationType } from "./IShiftParticipationType";
import { IShiftParticipation } from "./IShiftParticipation";
export interface IShiftCellsMatrixProps {
    startDate: Date;
    endDate: Date;
    shifts: IShift[];
    shiftTypes: IShiftType[];
    employees: IEmployee[];
    shiftParticipationTypes: IShiftParticipationType[];
    editShiftComments: (x: IShift) => void
    moveShiftParticipation: (x: IShiftParticipation, y: -1 | 1) => void
    updateShiftParticipation: (x: IShiftParticipation) => void
    removeShiftParticipation: (x: IShiftParticipation) => void
    createShiftParticipation: (x: IShift) => void
    createShiftParticipationFromGroup: (x: IShift) => void
    removeAllShiftParticipations: (x: IShift) => void
}
