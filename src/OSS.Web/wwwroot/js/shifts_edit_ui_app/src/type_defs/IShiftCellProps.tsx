import { IGroupedShiftParticipationType } from "./IGroupedShiftParticipationType";
import { IGroupedShiftType } from "./IGroupedShiftType";
import { IGroupedEmployee } from "./IGroupedEmployee";
import { IShift } from "./IShift";
import { IShiftParticipation } from "./IShiftParticipation";

export interface IShiftCellProps {
    colSize: number
    groupedShiftParticipationTypes: IGroupedShiftParticipationType;
    groupedShiftTypes: IGroupedShiftType;
    groupedEmployees: IGroupedEmployee;
    shift: IShift;
    editShiftComments: (x: IShift) => void
    moveShiftParticipation: (x: IShiftParticipation, y: -1 | 1) => void
    updateShiftParticipation: (x: IShiftParticipation) => void
    removeShiftParticipation: (x: IShiftParticipation) => void
    createShiftParticipation: (x: IShift) => void
    createShiftParticipationFromGroup: (x: IShift) => void
    removeAllShiftParticipations: (x: IShift) => void
}