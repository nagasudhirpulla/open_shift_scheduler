import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface IGetShiftParticipationTypesPayload {

}

export interface IGetShiftParticipationTypesAction extends IAction {
    type: ActionType.GET_SHIFT_PARTICIPATIONS,
    payload: IGetShiftParticipationTypesPayload
}

export function getShiftParticipationTypesAction(): IGetShiftParticipationTypesAction {
    return {
        type: ActionType.GET_SHIFT_PARTICIPATIONS,
        payload: {}
    };
}