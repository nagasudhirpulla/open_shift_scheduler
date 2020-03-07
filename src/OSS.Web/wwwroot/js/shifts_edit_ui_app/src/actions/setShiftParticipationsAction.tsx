import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { IShift } from "../type_defs/IShift";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

export interface ISetShiftParticipationsPayload {
    shiftParticipations: IShiftParticipation[]
}

export interface ISetShiftParticipationsAction extends IAction {
    type: ActionType.SET_SHIFT_PARTICIPATIONS,
    payload: ISetShiftParticipationsPayload
}

export function setShiftParticipationsAction(shiftParticipations: IShiftParticipation[]): ISetShiftParticipationsAction {
    return {
        type: ActionType.SET_SHIFT_PARTICIPATIONS,
        payload: { shiftParticipations }
    };
}

export const setShiftParticipationsReducer = (state: IShiftsEditUIState, action: ISetShiftParticipationsAction): IShiftsEditUIState => {
    // assumption: all participations belong to same shift
    // TODO check this and throw error if not true
    // find the relavent shift and add the participation object to it
    let shiftParticipations = action.payload.shiftParticipations;
    // console.log(shiftParticipation);
    let shiftInd = -1;
    if (shiftParticipations.length == 0) {
        console.log('No shift participations present, hence not changing the app state');
        return state;
    }
    for (let shiftIter = 0; (shiftIter < state.ui.shifts.length) && (shiftInd == -1); shiftIter++) {
        //console.log(state.shifts[shiftIter].shiftId);
        if (state.ui.shifts[shiftIter].id == shiftParticipations[0].shiftId) {
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
                        "shiftParticipations": [
                            ...shiftParticipations
                        ]
                    },
                    ...state.ui.shifts.slice(shiftInd + 1)
                ]
            }
        };
        // console.log(newState);
        return newState;
    }
    else {
        console.log('Could not find shift to add the shift participations');
        return state;
    }
}