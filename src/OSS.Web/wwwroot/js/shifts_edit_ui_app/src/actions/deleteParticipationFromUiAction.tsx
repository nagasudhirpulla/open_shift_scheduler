import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface IDeleteParticipationFromUiPayload {
    shiftParticipation: IShiftParticipation
}

export interface IDeleteParticipationFromUiAction extends IAction {
    type: ActionType.DELETE_SHIFT_PARTICIPATION_FROM_UI,
    payload: IDeleteParticipationFromUiPayload
}

export function deleteParticipationFromUiAction(shiftParticipation: IShiftParticipation): IDeleteParticipationFromUiAction {
    return {
        type: ActionType.DELETE_SHIFT_PARTICIPATION_FROM_UI,
        payload: { shiftParticipation }
    };
}

export const deleteParticipationFromUiReducer = (state: IShiftsEditUIState, action: IDeleteParticipationFromUiAction): IShiftsEditUIState => {
    // find the relavent shift and add the participation object to it
    const shiftParticipation = action.payload.shiftParticipation;
    // console.log(shiftParticipation);
    let shiftInd = -1;
    let shiftParticipationInd = -1;
    for (let shiftIter = 0; (shiftIter < state.ui.shifts.length) && (shiftInd == -1); shiftIter++) {
        //console.log(state.shifts[shiftIter].shiftId);
        if (state.ui.shifts[shiftIter].id == shiftParticipation.shiftId) {
            shiftInd = shiftIter;
            // find the shift participation index for the shift
            for (let partIter = 0; (partIter < state.ui.shifts[shiftInd].shiftParticipations.length) && (shiftParticipationInd == -1); partIter++) {
                if (state.ui.shifts[shiftInd].shiftParticipations[partIter].id == shiftParticipation.id) {
                    shiftParticipationInd = partIter;
                }
            }
        }
    }
    if (shiftInd != -1 && shiftParticipationInd != -1) {
        const newState = {
            ...state,
            ui: {
                ...state.ui,
                shifts: [
                    ...state.ui.shifts.slice(0, shiftInd),
                    {
                        ...state.ui.shifts[shiftInd],
                        "shiftParticipations": [
                            ...state.ui.shifts[shiftInd].shiftParticipations.slice(0, shiftParticipationInd),
                            ...state.ui.shifts[shiftInd].shiftParticipations.slice(shiftParticipationInd + 1),
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
        console.log('Could not find shift participation to delete in application state');
        return state;
    }
}