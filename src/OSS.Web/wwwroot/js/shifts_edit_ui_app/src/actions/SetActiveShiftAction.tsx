import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";

export interface ISetActiveShiftPayload {
    shift: IShift
}

export interface ISetActiveShiftAction extends IAction {
    type: ActionType.SET_ACTIVE_SHIFT,
    payload: ISetActiveShiftPayload
}

export function setActiveShiftAction(shift: IShift): ISetActiveShiftAction {
    return {
        type: ActionType.SET_ACTIVE_SHIFT,
        payload: { shift }
    };
}