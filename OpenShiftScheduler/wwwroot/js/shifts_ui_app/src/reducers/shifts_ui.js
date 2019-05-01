import initialState from './initialState';
import * as types from '../actions/actionTypes';

export default function shiftsUIReducer(state = initialState.shifts_ui, action) {
    switch (action.type) {
        case types.UPDATE_SHIFTS_UI_SHIFTS:
            return {
                ...state,
                shifts: action.shifts
            };
        case types.UPDATE_SHIFTS_UI_SHIFT_TYPES:
            return {
                ...state,
                shift_types: action.shift_types
            };
        case types.ADD_SHIFTS_UI_SHIFT_PARTICIPATION:
            // find the relavent shift and add the participation object to it
            const shiftParticipation = action.shift_participation;
            // console.log(shiftParticipation);
            let shiftInd = -1;
            for (let shiftIter = 0; (shiftIter < state.shifts.length) && (shiftInd == -1); shiftIter++) {
                //console.log(state.shifts[shiftIter].shiftId);
                if (state.shifts[shiftIter].shiftId == shiftParticipation.shiftId) {
                    shiftInd = shiftIter;
                }
            }
            if (shiftInd != -1) {
                const newState = {
                    ...state,
                    shifts: [
                        ...state.shifts.slice(0, shiftInd),
                        {
                            ...state.shifts[shiftInd],
                            "shiftParticipations": [
                                ...state.shifts[shiftInd]["shiftParticipations"],
                                shiftParticipation
                            ]
                        },
                        ...state.shifts.slice(shiftInd + 1)
                    ]
                };
                // console.log(newState);
                return newState;    
            }
            else {
                console.log('Could not find shift to add the shift participation');
                return state;
            }
        default:
            return state;
    }
}