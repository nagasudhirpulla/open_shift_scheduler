import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface ISetStartTimePayload {
    timeObj: Date
}

export interface ISetStartTimeAction extends IAction {
    type: ActionType.SET_START_TIME,
    payload: ISetStartTimePayload
}

export function setStartTimeAction(timeObj: Date): ISetStartTimeAction {
    return {
        type: ActionType.SET_START_TIME,
        payload: { timeObj }
    };
}