import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface ISetShiftsPayload {
    shifts: IShift[]
}

export interface ISetShiftsAction extends IAction {
    type: ActionType.SET_SHIFTS,
    payload: ISetShiftsPayload
}

export function setShiftsAction(shifts: IShift[]): ISetShiftsAction {
    return {
        type: ActionType.SET_SHIFTS,
        payload: { shifts }
    };
}

export const setShiftsReducer = (state: IShiftsEditUIState, action: ISetShiftsAction): IShiftsEditUIState => {
    return {
        ...state,
        ui: {
            ...state.ui,
            shifts: action.payload.shifts
        }
    } as IShiftsEditUIState;
}