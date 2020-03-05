import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";

export interface IDeleteShiftPayload {
    shift: IShift
}

export interface IDeleteShiftAction extends IAction {
    type: ActionType.DELETE_SHIFT,
    payload: IDeleteShiftPayload
}

export function deleteShiftAction(shift: IShift): IDeleteShiftAction {
    return {
        type: ActionType.DELETE_SHIFT,
        payload: { shift }
    };
}