import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface IAddParticipationToUiPayload {
    shiftParticipation: IShiftParticipation
}

export interface IAddParticipationToUiAction extends IAction {
    type: ActionType.ADD_PARTICIPATION_TO_UI,
    payload: IAddParticipationToUiPayload
}

export function addParticipationToUiAction(shiftParticipation: IShiftParticipation): IAddParticipationToUiAction {
    return {
        type: ActionType.ADD_PARTICIPATION_TO_UI,
        payload: { shiftParticipation }
    };
}

export const addParticipationToUiReducer = (state: IShiftsEditUIState, action: IAddParticipationToUiAction): IShiftsEditUIState => {
    // find the relavent shift and add the participation object to it
    let shiftParticipation = action.payload.shiftParticipation;
    let shiftInd = -1;
    for (let shiftIter = 0; (shiftIter < state.ui.shifts.length) && (shiftInd == -1); shiftIter++) {
        //console.log(state.shifts[shiftIter].shiftId);
        if (state.ui.shifts[shiftIter].id == shiftParticipation.shiftId) {
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
                        shiftParticipations: [
                            ...state.ui.shifts[shiftInd].shiftParticipations,
                            shiftParticipation
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
        console.log('Could not find shift to add the shift participation');
        return state;
    }
}