import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface IGetShiftsBetweenDatesPayload {
}

export interface IGetShiftsBetweenDatesAction extends IAction {
    type: ActionType.GET_SHIFTS_BETWEEN_DATES,
    payload: IGetShiftsBetweenDatesPayload
}

export function getShiftsBetweenDatesAction(): IGetShiftsBetweenDatesAction {
    return {
        type: ActionType.GET_SHIFTS_BETWEEN_DATES,
        payload: {}
    };
}