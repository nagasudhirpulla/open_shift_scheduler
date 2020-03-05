import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

export interface ICreateShiftParticipationPayload {
    shiftParticipation: IShiftParticipation
}

export interface ICreateShiftParticipationAction extends IAction {
    type: ActionType.CREATE_SHIFT_PARTICIPATION,
    payload: ICreateShiftParticipationPayload
}

export function createShiftParticipationAction(shiftParticipation: IShiftParticipation): ICreateShiftParticipationAction {
    return {
        type: ActionType.CREATE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation }
    };
}