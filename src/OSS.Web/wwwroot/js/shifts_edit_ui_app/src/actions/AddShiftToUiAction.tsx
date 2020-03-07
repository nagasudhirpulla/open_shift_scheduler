import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface IAddShiftToUiPayload {
    shift: IShift
}

export interface IAddShiftToUiAction extends IAction {
    type: ActionType.ADD_SHIFT_TO_UI,
    payload: IAddShiftToUiPayload
}

export function addShiftToUiAction(shift: IShift): IAddShiftToUiAction {
    return {
        type: ActionType.ADD_SHIFT_TO_UI,
        payload: { shift }
    };
}

export const addShiftToUiReducer = (state: IShiftsEditUIState, action: IAddShiftToUiAction): IShiftsEditUIState => {
    // console.log(`shift that is to be created in reducer is ${JSON.stringify(action.payload.shift)}`);
    const shift = action.payload.shift;
    if (shift.shiftParticipations == null) {
        shift.shiftParticipations = [];
    }
    const newState = {
        ...state,
        ui: {
            ...state.ui,
            shifts: [
                ...state.ui.shifts,
                shift
            ]
        }
    }
    return newState;
}