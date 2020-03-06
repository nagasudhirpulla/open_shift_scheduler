import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipationType } from "../type_defs/IShiftParticipationType";

export interface ISetShiftParticipationTypesPayload {
    shiftParticipationTypes: IShiftParticipationType[]
}

export interface ISetShiftParticipationTypesAction extends IAction {
    type: ActionType.SET_SHIFT_PARTICIPATION_TYPES,
    payload: ISetShiftParticipationTypesPayload
}

export function setShiftParticipationTypesAction(shiftParticipationTypes: IShiftParticipationType[]): ISetShiftParticipationTypesAction {
    return {
        type: ActionType.SET_SHIFT_PARTICIPATION_TYPES,
        payload: { shiftParticipationTypes }
    };
}