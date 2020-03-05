import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface ISetEndTimePayload {
    timeObj: Date
}

export interface ISetEndTimeAction extends IAction {
    type: ActionType.SET_END_TIME,
    payload: ISetEndTimePayload
}

export function setEndTimeAction(timeObj: Date): ISetEndTimeAction {
    return {
        type: ActionType.SET_END_TIME,
        payload: { timeObj }
    };
}