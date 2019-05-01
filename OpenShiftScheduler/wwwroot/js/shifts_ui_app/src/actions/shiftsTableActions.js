import { getShiftsForUI, createShiftParticipation as addShiftParticipation, createShift } from '../server_mediators/shifts_ui';
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

export function createShiftParticipation(baseAddr, employeeId, shift) {
    return async function (dispatch) {
        //todo if shift object is null, create shift in server, and then dispatch action to update shift object in redux state
        let shiftObj = shift;
        if (shift.shiftId == null) {
            shiftObj = await createShift(baseAddr, shift);
            // console.log(shiftObj);
            dispatch(createShiftUIShift(shiftObj));
        }
        const shift_participation = await addShiftParticipation(baseAddr, employeeId, shiftObj);
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

export function createShiftUIShift(shift) {
    //console.log(dashboardCell);
    return { type: types.CREATE_SHIFTS_UI_SHIFT, shift: shift };
}