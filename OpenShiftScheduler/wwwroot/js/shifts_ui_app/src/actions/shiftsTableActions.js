import { getShiftsForUI, createShiftParticipation as addShiftParticipation } from '../server_mediators/shifts_ui';
import { getShiftTypes } from '../server_mediators/shift_type';
import * as types from './actionTypes';

export function updateShiftsInUI(baseAddr, start_date, end_date) {
    return async function (dispatch) {
        const shifts = await getShiftsForUI(baseAddr, start_date, end_date);
        // console.log(shifts);
        dispatch(updateShiftsUIShifts(shifts));
    };

}

export function updateShiftTypesInUI(baseAddr) {
    return async function (dispatch) {
        const shift_types = await getShiftTypes(baseAddr);
        // console.log(shifts);
        dispatch(updateShiftsUIShiftTypes(shift_types));
    };

}

export function createShiftParticipation(baseAddr, employeeId, shiftId) {
    return async function (dispatch) {
        const shift_participation = await addShiftParticipation(baseAddr,employeeId, shiftId);
        // console.log(shift_participation);
        dispatch(addShiftUIShiftParticipation(shift_participation));
    };

}

export function updateShiftsUIShifts(shifts) {
    //console.log(dashboardCell);
    return { type: types.UPDATE_SHIFTS_UI_SHIFTS, shifts: shifts };
}

export function updateShiftsUIShiftTypes(shift_types) {
    //console.log(dashboardCell);
    return { type: types.UPDATE_SHIFTS_UI_SHIFT_TYPES, shift_types: shift_types };
}

export function addShiftUIShiftParticipation(shift_participation) {
    //console.log(dashboardCell);
    return { type: types.ADD_SHIFTS_UI_SHIFT_PARTICIPATION, shift_participation: shift_participation };
}