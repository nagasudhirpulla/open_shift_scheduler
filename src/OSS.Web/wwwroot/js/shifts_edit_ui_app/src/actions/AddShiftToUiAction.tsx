import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";

export interface IAddShiftToUiPayload {
    shift: IShift
}

export interface IAddShiftToUiAction extends IAction {
    type: ActionType.ADD_SHIFT_TO_UI,
    payload: IAddShiftToUiPayload
}

export function addShiftToUiAction(shift: IShift): IAddShiftToUiAction {
    return {
        type: ActionType.ADD_SHIFT_TO_UI,
        payload: { shift }
    };
}