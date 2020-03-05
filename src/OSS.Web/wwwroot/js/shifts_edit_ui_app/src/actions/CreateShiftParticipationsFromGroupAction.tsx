import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface ICreateShiftParticipationsFromGroupPayload {
    shiftId: number,
    shiftGroupId: number
}

export interface ICreateShiftParticipationsFromGroupAction extends IAction {
    type: ActionType.CREATE_SHIFT_PARTICIPATIONS_FROM_GROUP,
    payload: ICreateShiftParticipationsFromGroupPayload
}

export function getShiftTypesAction(shiftId: number, shiftGroupId: number): ICreateShiftParticipationsFromGroupAction {
    return {
        type: ActionType.CREATE_SHIFT_PARTICIPATIONS_FROM_GROUP,
        payload: { shiftId, shiftGroupId }
    };
}