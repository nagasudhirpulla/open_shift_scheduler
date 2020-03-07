import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface IGetShiftsForUiPayload {
}

export interface IGetShiftsForUiAction extends IAction {
    type: ActionType.GET_SHIFTS_FOR_UI,
    payload: IGetShiftsForUiPayload
}

export function getShiftsForUiAction(): IGetShiftsForUiAction {
    return {
        type: ActionType.GET_SHIFTS_FOR_UI,
        payload: {}
    };
}