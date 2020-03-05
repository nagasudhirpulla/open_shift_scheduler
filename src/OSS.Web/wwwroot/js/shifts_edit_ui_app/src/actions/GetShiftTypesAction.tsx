import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface IGetShiftTypesPayload {

}

export interface IGetShiftTypesAction extends IAction {
    type: ActionType.GET_SHIFT_TYPES,
    payload: IGetShiftTypesPayload
}

export function getShiftTypesAction(): IGetShiftTypesAction {
    return {
        type: ActionType.GET_SHIFT_TYPES,
        payload: {}
    };
}