import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";

export interface ISetShiftsPayload {
    shifts: IShift[]
}

export interface ISetShiftsAction extends IAction {
    type: ActionType.SET_SHIFTS,
    payload: ISetShiftsPayload
}

export function setShiftsAction(shifts: IShift[]): ISetShiftsAction {
    return {
        type: ActionType.SET_SHIFTS,
        payload: { shifts }
    };
}