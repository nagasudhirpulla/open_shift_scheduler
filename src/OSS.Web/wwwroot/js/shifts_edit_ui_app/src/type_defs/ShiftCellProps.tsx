import { IGroupedShiftParticipationType } from "./IGroupedShiftParticipationType";
import { IGroupedShiftType } from "./IGroupedShiftType";
import { IGroupedEmployee } from "./IGroupedEmployee";
import { IShift } from "./IShift";

export interface IShiftCellProps {
    groupedShiftParticipationTypes: IGroupedShiftParticipationType;
    groupedShiftTypes: IGroupedShiftType;
    groupedEmployees: IGroupedEmployee;
    shift: IShift;
}