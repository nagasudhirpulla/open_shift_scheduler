import { IShift } from "./IShift";
import { IShiftGroup } from "./IShiftGroup";
import { IShiftParticipationType } from "./IShiftParticipationType";
import { IShiftParticipation } from "./IShiftParticipation";
import { IEmployee } from "./IEmployee";
export interface IAddFromShiftGroupModalProps {
    show: boolean;
    setShow: (x: boolean) => void;
    shift: IShift;
    shiftGroups: IShiftGroup[];
    onShiftGroupSubmit: (shift: IShift, shiftGroupId: number) => void;
}
