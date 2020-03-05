import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface IGetShiftGroupsPayload {

}

export interface IGetShiftGroupsAction extends IAction {
    type: ActionType.GET_SHIFT_GROUPS,
    payload: IGetShiftGroupsPayload
}

export function getShiftGroupsAction(): IGetShiftGroupsAction {
    return {
        type: ActionType.GET_SHIFT_GROUPS,
        payload: {}
    };
}