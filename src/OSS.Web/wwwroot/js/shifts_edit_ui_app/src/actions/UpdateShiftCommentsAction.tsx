import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface IUpdateShiftCommentsPayload {
    shiftId: number,
    comment: string
}

export interface IUpdateShiftCommentsAction extends IAction {
    type: ActionType.UPDATE_SHIFT_COMMENTS,
    payload: IUpdateShiftCommentsPayload
}

export function updateShiftCommentsAction(shiftId: number, comment: string): IUpdateShiftCommentsAction {
    return {
        type: ActionType.UPDATE_SHIFT_COMMENTS,
        payload: { shiftId, comment }
    };
}

export const updateShiftCommentsReducer = (state: IShiftsEditUIState, action: IUpdateShiftCommentsAction): IShiftsEditUIState => {
    // find the target shift
    let shiftInd = -1;
    for (let shiftIter = 0; (shiftIter < state.ui.shifts.length) && (shiftInd == -1); shiftIter++) {
        //console.log(state.shifts[shiftIter].shiftId);
        if (state.ui.shifts[shiftIter].id == action.payload.shiftId) {
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
                    {
                        ...state.ui.shifts[shiftInd],
                        comments: action.payload.comment
                    },
                    ...state.ui.shifts.slice(shiftInd + 1)
                ]
            }
        }
        return newState;
    }
    return state;
}