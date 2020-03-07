import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface IUpdateShiftParticipationPayload {
    shiftParticipation: IShiftParticipation
}

export interface IUpdateShiftParticipationAction extends IAction {
    type: ActionType.UPDATE_SHIFT_PARTICIPATION,
    payload: IUpdateShiftParticipationPayload
}

export function updateShiftParticipationAction(shiftParticipation: IShiftParticipation): IUpdateShiftParticipationAction {
    return {
        type: ActionType.UPDATE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation: shiftParticipation }
    };
}

export const updateShiftParticipationReducer = (state: IShiftsEditUIState, action: IUpdateShiftParticipationAction): IShiftsEditUIState => {
    // find the target shift
    const shiftParticipation = action.payload.shiftParticipation;
    // console.log(shiftParticipation);
    let shiftInd = -1;
    for (let shiftIter = 0; (shiftIter < state.ui.shifts.length) && (shiftInd == -1); shiftIter++) {
        //console.log(state.shifts[shiftIter].shiftId);
        if (state.ui.shifts[shiftIter].id == shiftParticipation.shiftId) {
            shiftInd = shiftIter;
        }
    }
    // find the target shift participation
    let shiftPartInd = -1;
    for (let shiftPartIter = 0; (shiftPartIter < state.ui.shifts[shiftInd].shiftParticipations.length) && (shiftPartInd == -1); shiftPartIter++) {
        if (shiftParticipation.id == state.ui.shifts[shiftInd].shiftParticipations[shiftPartIter].id) {
            shiftPartInd = shiftPartIter;
        }
    }
    if (shiftInd != -1 && shiftPartInd != -1) {
        const newState = {
            ...state,
            ui: {
                ...state.ui,
                shifts: [
                    ...state.ui.shifts.slice(0, shiftInd),
                    {
                        ...state.ui.shifts[shiftInd],
                        shiftParticipations: [
                            ...state.ui.shifts[shiftInd].shiftParticipations.slice(0, shiftPartInd),
                            shiftParticipation,
                            ...state.ui.shifts[shiftInd].shiftParticipations.slice(shiftPartInd + 1)
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
        console.log('Could not find shift participation index to edit');
        return state;
    }
}