import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftType } from "../type_defs/IShiftType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface ISetShiftTypesPayload {
    shiftTypes: IShiftType[]
}

export interface ISetShiftTypesAction extends IAction {
    type: ActionType.SET_SHIFT_TYPES,
    payload: ISetShiftTypesPayload
}

export function setShiftTypesAction(shiftTypes: IShiftType[]): ISetShiftTypesAction {
    return {
        type: ActionType.SET_SHIFT_TYPES,
        payload: { shiftTypes }
    };
}

export const setShiftTypesReducer = (state: IShiftsEditUIState, action: ISetShiftTypesAction): IShiftsEditUIState => {
    return {
        ...state,
        ui: {
            ...state.ui,
            shiftTypes: action.payload.shiftTypes
        }
    } as IShiftsEditUIState;
}