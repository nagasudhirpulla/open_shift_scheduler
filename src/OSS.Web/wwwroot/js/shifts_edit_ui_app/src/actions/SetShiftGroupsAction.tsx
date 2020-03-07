import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { IShiftGroup } from "../type_defs/IShiftGroup";

export interface ISetShiftGroupsPayload {
    shiftGroups: IShiftGroup[]
}

export interface ISetShiftGroupsAction extends IAction {
    type: ActionType.SET_SHIFT_GROUPS,
    payload: ISetShiftGroupsPayload
}

export function setShiftGroupsAction(shiftGroups: IShiftGroup[]): ISetShiftGroupsAction {
    return {
        type: ActionType.SET_SHIFT_GROUPS,
        payload: { shiftGroups }
    };
}

export const setShiftGroupsReducer = (state: IShiftsEditUIState, action: ISetShiftGroupsAction): IShiftsEditUIState => {
    return {
        ...state,
        ui: {
            ...state.ui,
            shiftGroups: action.payload.shiftGroups
        }
    } as IShiftsEditUIState;
}