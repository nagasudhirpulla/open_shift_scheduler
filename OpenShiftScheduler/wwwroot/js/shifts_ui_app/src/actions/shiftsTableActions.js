import { getShiftsForUI, createServerShiftParticipation, addShiftParticipationFromShiftGroup, deleteShiftParticipation, createShift, deleteShift, updateServerShiftComments } from '../server_mediators/shifts_ui';
import { getEmployees } from '../server_mediators/employee';
import { getShiftTypes } from '../server_mediators/shift_type';
import { getShiftParticipationTypes } from '../server_mediators/shift_participation_type';
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
        // console.log(shift_types);
        dispatch(updateShiftsUIShiftTypes(shift_types));
    };

}

export function updateEmployeesInUI(baseAddr) {
    return async function (dispatch) {
        const employees = await getEmployees(baseAddr);
        // console.log(employees);
        dispatch(updateShiftsUIEmployees(employees));
    };

}

export function updateShiftsUIData(baseAddr, start_date, end_date) {
    return async function (dispatch) {
        // update employees
        const employees = await getEmployees(baseAddr);
        // console.log(employees);
        dispatch(updateShiftsUIEmployees(employees));

        // update shift types
        const shift_types = await getShiftTypes(baseAddr);
        // console.log(shift_types);
        dispatch(updateShiftsUIShiftTypes(shift_types));

        // update shift participation types
        const shift_participation_types = await getShiftParticipationTypes(baseAddr);
        // console.log(shift_part_types);
        dispatch(updateShiftsUIShiftParticipationTypes(shift_participation_types));

        // update shifts
        const shifts = await getShiftsForUI(baseAddr, start_date, end_date);
        // console.log(shifts);
        dispatch(updateShiftsUIShifts(shifts));
    };
}

export function createShiftParticipation(baseAddr, employeeId, shift) {
    return async function (dispatch) {
        let shiftObj = shift;
        if (shift.shiftId == null) {
            shiftObj = await createShift(baseAddr, shift);
            // console.log(shiftObj);
            dispatch(createShiftUIShift(shiftObj));
        }
        const shift_participation = await createServerShiftParticipation(baseAddr, employeeId, shiftObj);
        // console.log(shift_participation);
        dispatch(addShiftUIShiftParticipation(shift_participation));
    };

}

export function createShiftParticipationFromGroup(baseAddr, shiftGroupId, shift) {
    return async function (dispatch) {
        let shiftObj = shift;
        if (shift.shiftId == null) {
            shiftObj = await createShift(baseAddr, shift);
            // console.log(shiftObj);
            dispatch(createShiftUIShift(shiftObj));
        }
        const shift_participations = await addShiftParticipationFromShiftGroup(baseAddr, shiftGroupId, shiftObj);
        // console.log(shift_participation);
        dispatch(updateShiftUIShiftParticipations(shift_participations));
    };

}

export function removeShiftParticipation(baseAddr, shiftParticipation) {
    return async function (dispatch) {
        if (shiftParticipation.shiftParticipationId != null) {
            const shiftParticipationObj = await deleteShiftParticipation(baseAddr, shiftParticipation);
            // console.log(shiftParticipationObj);
            dispatch(deleteShiftUIShiftParticipation(shiftParticipationObj));
        }
    };
}

export function removeShift(baseAddr, shift) {
    return async function (dispatch) {
        if (shift.shiftId != null) {
            const shiftObj = await deleteShift(baseAddr, shift);
            // console.log(shiftParticipationObj);
            dispatch(removeShiftUIShift(shiftObj));
        }
    };
}

export function updateShiftComments(baseAddr, shift) {
    return async function (dispatch) {
        let shiftObj = shift;
        if (shift.shiftId == null) {
            shiftObj = await createShift(baseAddr, shift);
            // console.log(shiftObj);
            dispatch(createShiftUIShift(shiftObj));
            if (res.success == undefined || res.success == false) {
                console.log("unable to create shif at server, recieved undesirable response " + JSON.stringify(res));
                return;
            }
        }
        const res = await updateServerShiftComments(baseAddr, shiftObj);
        // console.log(res);
        if (res.success != undefined && res.success != false) {
            dispatch(updateShiftUIShiftComments(shiftObj));
        } else {
            console.log("On update shift comments action at server, recieved undesirable response " + JSON.stringify(res));
        }
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

export function updateShiftsUIEmployees(employees) {
    return { type: types.UPDATE_SHIFTS_UI_EMPLOYEES, employees: employees };
}

export function addShiftUIShiftParticipation(shift_participation) {
    //console.log(dashboardCell);
    return { type: types.ADD_SHIFTS_UI_SHIFT_PARTICIPATION, shift_participation: shift_participation };
}

export function updateShiftUIShiftParticipations(shift_participations) {
    //console.log(dashboardCell);
    return { type: types.UPDATE_SHIFTS_UI_SHIFT_PARTICIPATIONS, shift_participations: shift_participations };
}

export function deleteShiftUIShiftParticipation(shift_participation) {
    //console.log(dashboardCell);
    return { type: types.DELETE_SHIFTS_UI_SHIFT_PARTICIPATION, shift_participation: shift_participation };
}

export function createShiftUIShift(shift) {
    //console.log(dashboardCell);
    return { type: types.CREATE_SHIFTS_UI_SHIFT, shift: shift };
}

export function removeShiftUIShift(shift) {
    //console.log(dashboardCell);
    return { type: types.DELETE_SHIFTS_UI_SHIFT, shift: shift };
}

export function updateShiftUIShiftComments(shift) {
    //console.log(shift);
    return { type: types.UPDATE_SHIFTS_UI_SHIFT_COMMENTS, shift: shift };
}

export function updateShiftsUIShiftParticipationTypes(shift_participation_types) {
    return { type: types.UPDATE_SHIFTS_UI_SHIFT_PARTICIPATION_TYPES, shift_participation_types: shift_participation_types };
}
