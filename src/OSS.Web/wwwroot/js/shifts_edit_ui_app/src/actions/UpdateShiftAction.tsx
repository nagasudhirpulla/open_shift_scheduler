import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface IUpdateShiftPayload {
    shift: IShift
}

export interface IUpdateShiftAction extends IAction {
    type: ActionType.UPDATE_SHIFT,
    payload: IUpdateShiftPayload
}

export function updateShiftAction(shift: IShift): IUpdateShiftAction {
    return {
        type: ActionType.UPDATE_SHIFT,
        payload: { shift }
    };
}

export const updateShiftReducer = (state: IShiftsEditUIState, action: IUpdateShiftAction): IShiftsEditUIState => {
    // find the target shift
    let shiftInd = -1;
    for (let shiftIter = 0; (shiftIter < state.ui.shifts.length) && (shiftInd == -1); shiftIter++) {
        //console.log(state.shifts[shiftIter].shiftId);
        if (state.ui.shifts[shiftIter].id == action.payload.shift.id) {
            shiftInd = shiftIter;
        }
    }
    if (shiftInd != -1) {
        const newState = {
            ...state,
            ui: {
                ...state.ui,
                shifts: [
                    ...state.ui.shifts.slice(0, shiftInd),
                    action.payload.shift,
                    ...state.ui.shifts.slice(shiftInd + 1)
                ]
            }
        }
        return newState;
    }
    return state;
}