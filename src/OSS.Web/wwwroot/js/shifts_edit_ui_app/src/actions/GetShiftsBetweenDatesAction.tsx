import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface IGetShiftsBetweenDatesPayload {
    startDate: Date;
    endDate: Date;
}

export interface IGetShiftsBetweenDatesAction extends IAction {
    type: ActionType.GET_SHIFTS_BETWEEN_DATES,
    payload: IGetShiftsBetweenDatesPayload
}

export function getShiftsBetweenDatesAction(startDate: Date, endDate: Date): IGetShiftsBetweenDatesAction {
    return {
        type: ActionType.GET_SHIFTS_BETWEEN_DATES,
        payload: { startDate, endDate }
    };
}