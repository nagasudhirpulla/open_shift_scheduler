import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

export interface IUpdateShiftParticipationPayload {
    shiftParticipation: IShiftParticipation
}

export interface IUpdateShiftParticipationAction extends IAction {
    type: ActionType.UPDATE_SHIFT_PARTICIPATION,
    payload: IUpdateShiftParticipationPayload
}

export function updateShiftParticipationAction(shiftParticipation: IShiftParticipation): IUpdateShiftParticipationAction {
    return {
        type: ActionType.UPDATE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation: shiftParticipation }
    };
}