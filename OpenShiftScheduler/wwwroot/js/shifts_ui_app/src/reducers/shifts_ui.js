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
        case types.UPDATE_SHIFTS_UI_SHIFT:
            // find the shift index from shifts array using date and shift type
            // console.log(`shift that is to be updated in reducer is ${JSON.stringify(action.shift)}`);
            let shift = action.shift;
            let reqShiftIter = -1;
            for (let shiftIter = 0; (shiftIter < state.shifts.length) && (reqShiftIter == -1); shiftIter++) {
                if (state.shifts[shiftIter].shiftId == shift.shiftId) {
                    reqShiftIter = shiftIter;
                }
            }
            if (reqShiftIter == -1) {
                console.log(`Could not find the requested shift ${shift} to update in the redux state`);
                return state;
            }
            let newState = {
                ...state,
                shifts:
                    [
                        ...state.shifts.slice(0, reqShiftIter),
                        shift,
                        ...state.shifts.slice(reqShiftIter + 1)
                    ]
            }
            return newState;
        case types.CREATE_SHIFTS_UI_SHIFT:
            // console.log(`shift that is to be created in reducer is ${JSON.stringify(action.shift)}`);
            shift = action.shift;
            if (shift.shiftParticipations == null) {
                shift.shiftParticipations = [];
            }
            newState = {
                ...state,
                shifts:
                    [
                        ...state.shifts,
                        shift
                    ]
            }
            return newState;
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