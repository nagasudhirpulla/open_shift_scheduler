import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";

export interface ICreateShiftPayload {
    shift: IShift
}

export interface ICreateShiftAction extends IAction {
    type: ActionType.CREATE_SHIFT,
    payload: ICreateShiftPayload
}

export function createShiftAction(shift: IShift): ICreateShiftAction {
    return {
        type: ActionType.CREATE_SHIFT,
        payload: { shift }
    };
}