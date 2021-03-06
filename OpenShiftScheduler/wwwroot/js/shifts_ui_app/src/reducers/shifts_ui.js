import initialState from './initialState';
import * as types from '../actions/actionTypes';
import { UPDATE_SHIFTS_UI_SHIFT_COMMENTS } from '../actions/actionTypes';

export default function shiftsUIReducer(state = initialState.shifts_ui, action) {
    switch (action.type) {
        case types.UPDATE_SHIFTS_UI_SHIFTS:
            return {
                ...state,
                shifts: action.shifts
            };
        case types.DELETE_SHIFTS_UI_SHIFT:
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
                        ...state.shifts.slice(reqShiftIter + 1)
                    ]
            }
            return newState;
        case types.UPDATE_SHIFTS_UI_SHIFT_TYPES:
            return {
                ...state,
                shift_types: action.shift_types
            };
        case types.UPDATE_SHIFTS_UI_EMPLOYEES:
            return {
                ...state,
                employees: action.employees
            };
        case types.UPDATE_SHIFTS_UI_SHIFT:
            // find the shift index from shifts array using date and shift type
            // console.log(`shift that is to be updated in reducer is ${JSON.stringify(action.shift)}`);
            shift = action.shift;
            reqShiftIter = -1;
            for (let shiftIter = 0; (shiftIter < state.shifts.length) && (reqShiftIter == -1); shiftIter++) {
                if (state.shifts[shiftIter].shiftId == shift.shiftId) {
                    reqShiftIter = shiftIter;
                }
            }
            if (reqShiftIter == -1) {
                console.log(`Could not find the requested shift ${shift} to update in the redux state`);
                return state;
            }
            newState = {
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
            let shiftParticipation = action.shift_participation;
            // console.log(shiftParticipation);
            let shiftInd = -1;
            for (let shiftIter = 0; (shiftIter < state.shifts.length) && (shiftInd == -1); shiftIter++) {
                //console.log(state.shifts[shiftIter].shiftId);
                if (state.shifts[shiftIter].shiftId == shiftParticipation.shiftId) {
                    shiftInd = shiftIter;
                }
            }
            if (shiftInd != -1) {
                newState = {
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
        case types.UPDATE_SHIFTS_UI_SHIFT_PARTICIPATIONS:
            // find the relavent shift and add the participation object to it
            let shiftParticipations = action.shift_participations;
            // console.log(shiftParticipation);
            shiftInd = -1;
            if (shiftParticipations.length == 0) {
                console.log('No shift participations present, hence not changing the app state');
                return state;
            }
            for (let shiftIter = 0; (shiftIter < state.shifts.length) && (shiftInd == -1); shiftIter++) {
                //console.log(state.shifts[shiftIter].shiftId);
                if (state.shifts[shiftIter].shiftId == shiftParticipations[0].shiftId) {
                    shiftInd = shiftIter;
                }
            }
            if (shiftInd != -1) {
                newState = {
                    ...state,
                    shifts: [
                        ...state.shifts.slice(0, shiftInd),
                        {
                            ...state.shifts[shiftInd],
                            "shiftParticipations": [
                                ...shiftParticipations
                            ]
                        },
                        ...state.shifts.slice(shiftInd + 1)
                    ]
                };
                // console.log(newState);
                return newState;
            }
            else {
                console.log('Could not find shift to add the shift participations');
                return state;
            }
        case types.UPDATE_SHIFTS_UI_SHIFT_PARTICIPATION:
            // find the target shift
            shiftParticipation = action.shift_participation;
            // console.log(shiftParticipation);
            shiftInd = -1;
            for (let shiftIter = 0; (shiftIter < state.shifts.length) && (shiftInd == -1); shiftIter++) {
                //console.log(state.shifts[shiftIter].shiftId);
                if (state.shifts[shiftIter].shiftId == shiftParticipation.shiftId) {
                    shiftInd = shiftIter;
                }
            }
            // find the target shift participation
            let shiftPartInd = -1;
            for (let shiftPartIter = 0; (shiftPartIter < state.shifts[shiftInd].shiftParticipations.length) && (shiftPartInd == -1); shiftPartIter++) {
                if (shiftParticipation.shiftParticipationId == state.shifts[shiftInd].shiftParticipations[shiftPartIter].shiftParticipationId) {
                    shiftPartInd = shiftPartIter;
                }
            }
            if (shiftInd != -1 && shiftPartInd != -1) {
                newState = {
                    ...state,
                    shifts: [
                        ...state.shifts.slice(0, shiftInd),
                        {
                            ...state.shifts[shiftInd],
                            "shiftParticipations": [
                                ...state.shifts[shiftInd].shiftParticipations.slice(0, shiftPartInd),
                                shiftParticipation,
                                ...state.shifts[shiftInd].shiftParticipations.slice(shiftPartInd + 1)
                            ]
                        },
                        ...state.shifts.slice(shiftInd + 1)
                    ]
                };
                // console.log(newState);
                return newState;
            }
            else {
                console.log('Could not find shift participation index to edit');
                return state;
            }
        case types.DELETE_SHIFTS_UI_SHIFT_PARTICIPATION:
            // find the relavent shift and add the participation object to it
            shiftParticipation = action.shift_participation;
            // console.log(shiftParticipation);
            shiftInd = -1;
            let shiftParticipationInd = -1;
            for (let shiftIter = 0; (shiftIter < state.shifts.length) && (shiftInd == -1); shiftIter++) {
                //console.log(state.shifts[shiftIter].shiftId);
                if (state.shifts[shiftIter].shiftId == shiftParticipation.shiftId) {
                    shiftInd = shiftIter;
                    // find the shift participation index for the shift
                    for (let partIter = 0; (partIter < state.shifts[shiftInd].shiftParticipations.length) && (shiftParticipationInd == -1); partIter++) {
                        if (state.shifts[shiftInd].shiftParticipations[partIter].shiftParticipationId == shiftParticipation.shiftParticipationId) {
                            shiftParticipationInd = partIter;
                        }
                    }
                }
            }
            if (shiftInd != -1 && shiftParticipationInd != -1) {
                newState = {
                    ...state,
                    shifts: [
                        ...state.shifts.slice(0, shiftInd),
                        {
                            ...state.shifts[shiftInd],
                            "shiftParticipations": [
                                ...state.shifts[shiftInd].shiftParticipations.slice(0, shiftParticipationInd),
                                ...state.shifts[shiftInd].shiftParticipations.slice(shiftParticipationInd + 1),
                            ]
                        },
                        ...state.shifts.slice(shiftInd + 1)
                    ]
                };
                // console.log(newState);
                return newState;
            }
            else {
                console.log('Could not find shift participation to delete in application state');
                return state;
            }
        case UPDATE_SHIFTS_UI_SHIFT_COMMENTS:
            // find the relavent shift object to update the comments
            shift = action.shift;
            // console.log(shift);
            shiftInd = -1;
            for (let shiftIter = 0; (shiftIter < state.shifts.length) && (shiftInd == -1); shiftIter++) {
                //console.log(state.shifts[shiftIter].shiftId);
                if (state.shifts[shiftIter].shiftId == shift.shiftId) {
                    shiftInd = shiftIter;
                }
            }
            if (shiftInd != -1) {
                newState = {
                    ...state,
                    shifts: [
                        ...state.shifts.slice(0, shiftInd),
                        {
                            ...state.shifts[shiftInd],
                            "comments": (shift.comments == undefined || shift.comments == null) ? "" : shift.comments
                        },
                        ...state.shifts.slice(shiftInd + 1)
                    ]
                };
                // console.log(newState);
                return newState;
            }
            else {
                console.log('Could not find shift to update the comments');
                return state;
            }
        case types.UPDATE_SHIFTS_UI_SHIFT_PARTICIPATION_TYPES:
            return {
                ...state,
                shift_participation_types: action.shift_participation_types
            };
        case types.UPDATE_SHIFTS_UI_SHIFT_GROUPS:
            return {
                ...state,
                shift_groups: action.shift_groups
            };
        default:
            return state;

    }
}