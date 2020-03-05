import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";

export interface IUpdateShiftCommentsPayload {
    shift: IShift
}

export interface IUpdateShiftCommentsAction extends IAction {
    type: ActionType.UPDATE_SHIFT_COMMENTS,
    payload: IUpdateShiftCommentsPayload
}

export function updateShiftCommentsAction(shift: IShift): IUpdateShiftCommentsAction {
    return {
        type: ActionType.UPDATE_SHIFT_COMMENTS,
        payload: { shift }
    };
}