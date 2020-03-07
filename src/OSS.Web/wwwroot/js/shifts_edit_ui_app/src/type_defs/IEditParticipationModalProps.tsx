import { IEmployee } from "./IEmployee";
import { IShiftParticipationType } from "./IShiftParticipationType";
import { IShift } from "./IShift";
import { IShiftParticipation } from "./IShiftParticipation";
import { IGroupedEmployee } from "./IGroupedEmployee";
import { IGroupedShiftParticipationType } from "./IGroupedShiftParticipationType";
export interface IEditParticipationModalProps {
    show: boolean;
    setShow: (x: boolean) => void;
    participation: IShiftParticipation;
    employees: IEmployee[];
    shiftParticipationTypes: IShiftParticipationType[];
    onParticipationSubmit: (sp: IShiftParticipation) => void;
}
