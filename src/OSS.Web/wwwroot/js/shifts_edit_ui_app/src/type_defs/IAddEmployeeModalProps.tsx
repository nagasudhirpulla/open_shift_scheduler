import { IEmployee } from "./IEmployee";
import { IShiftParticipationType } from "./IShiftParticipationType";
import { IShift } from "./IShift";
import { IShiftParticipation } from "./IShiftParticipation";

export interface IAddEmployeeModalProps {
    show: boolean;
    setShow: (x: boolean) => void;
    shift: IShift;
    employees: IEmployee[];
    shiftParticipationTypes: IShiftParticipationType[];
    onParticipationSubmit: (p: IShiftParticipation) => void;
}
