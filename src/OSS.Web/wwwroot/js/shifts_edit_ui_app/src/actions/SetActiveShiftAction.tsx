import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

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

export const setActiveShiftReducer = (state: IShiftsEditUIState, action: ISetActiveShiftAction): IShiftsEditUIState => {
    return {
        ...state,
        ui: {
            ...state.ui,
            activeShift: action.payload.shift
        }
    } as IShiftsEditUIState;
}