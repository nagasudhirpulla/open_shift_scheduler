import { IShift } from "./IShift";
import { IShiftType } from "./IShiftType";
export interface ICommentsElementProps {
    shifts: IShift[];
    shiftTypes: IShiftType[];
    editShiftComments: (x: IShift) => void
}
