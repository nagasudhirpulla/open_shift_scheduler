import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

export interface IDeleteShiftParticipationPayload {
    shiftParticipation: IShiftParticipation
}

export interface IDeleteShiftParticipationAction extends IAction {
    type: ActionType.DELETE_SHIFT_PARTICIPATION,
    payload: IDeleteShiftParticipationPayload
}

export function deleteShiftParticipationAction(shiftParticipation: IShiftParticipation): IDeleteShiftParticipationAction {
    return {
        type: ActionType.DELETE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation }
    };
}