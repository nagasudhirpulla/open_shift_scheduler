import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

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

export const setEndTimeReducer = (state: IShiftsEditUIState, action: ISetEndTimeAction): IShiftsEditUIState => {
    return {
        ...state,
        ui: {
            ...state.ui,
            endDate: action.payload.timeObj
        }
    }
}