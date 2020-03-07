import { ActionType } from "../actions/ActionType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { IAction } from "../type_defs/IAction";
import { ISetEmployeesAction, setEmployeesAction } from "../actions/SetEmployeesAction";
import { useReducer, useEffect, useCallback } from "react";
import { getEmployees } from "../server_mediators/employees";
import { ISetStartTimeAction } from "../actions/SetStartTimeAction";
import { ISetEndTimeAction } from "../actions/SetEndTimeAction";
import { getShiftsBetweenDates, createShiftParticipation, createShift } from "../server_mediators/shifts";
import { setShiftsAction, ISetShiftsAction } from "../actions/SetShiftsAction";
import { getShiftTypes } from "../server_mediators/shiftTypes";
import { setShiftTypesAction, ISetShiftTypesPayload, ISetShiftTypesAction } from "../actions/SetShiftTypesAction";
import { getShiftParticipationTypes } from "../server_mediators/shiftParticipationTypes";
import { setShiftParticipationTypesAction, ISetShiftParticipationTypesAction } from "../actions/SetShiftParticipationTypesAction";
import { ISetActiveShiftAction } from "../actions/SetActiveShiftAction";
import { ICreateShiftParticipationAction } from "../actions/CreateShiftParticipationAction";
import { IAddParticipationToUiAction, addParticipationToUiAction } from "../actions/AddParticipationToUiAction";
import { addShiftToUiAction, IAddShiftToUiAction } from "../actions/AddShiftToUiAction";

export const useShiftsEditUIReducer = (initState: IShiftsEditUIState): [IShiftsEditUIState, React.Dispatch<IAction>] => {
    // create the reducer function
    const reducer = (state: IShiftsEditUIState, action: IAction) => {
        switch (action.type) {
            case ActionType.SET_START_TIME:
                return {
                    ...state,
                    ui: {
                        ...state.ui,
                        startDate: (action as ISetStartTimeAction).payload.timeObj
                    }
                } as IShiftsEditUIState;
            case ActionType.SET_END_TIME:
                return {
                    ...state,
                    ui: {
                        ...state.ui,
                        endDate: (action as ISetEndTimeAction).payload.timeObj
                    }
                } as IShiftsEditUIState;
            case ActionType.SET_EMPLOYEES:
                return {
                    ...state,
                    ui: {
                        ...state.ui,
                        employees: (action as ISetEmployeesAction).payload.employees
                    }
                } as IShiftsEditUIState;
            case ActionType.SET_SHIFTS:
                return {
                    ...state,
                    ui: {
                        ...state.ui,
                        shifts: (action as ISetShiftsAction).payload.shifts
                    }
                } as IShiftsEditUIState;
            case ActionType.SET_SHIFT_TYPES:
                return {
                    ...state,
                    ui: {
                        ...state.ui,
                        shiftTypes: (action as ISetShiftTypesAction).payload.shiftTypes
                    }
                } as IShiftsEditUIState;
            case ActionType.SET_SHIFT_PARTICIPATION_TYPES:
                return {
                    ...state,
                    ui: {
                        ...state.ui,
                        shiftParticipationTypes: (action as ISetShiftParticipationTypesAction).payload.shiftParticipationTypes
                    }
                } as IShiftsEditUIState;
            case ActionType.SET_ACTIVE_SHIFT:
                return {
                    ...state,
                    ui: {
                        ...state.ui,
                        activeShift: (action as ISetActiveShiftAction).payload.shift
                    }
                } as IShiftsEditUIState;
            case ActionType.ADD_PARTICIPATION_TO_UI:
                // find the relavent shift and add the participation object to it
                let shiftParticipation = (action as IAddParticipationToUiAction).payload.shiftParticipation;
                // console.log(shiftParticipation);
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
                    };
                    // console.log(newState);
                    return newState as IShiftsEditUIState;
                }
                else {
                    console.log('Could not find shift to add the shift participation');
                    return state;
                }
            case ActionType.ADD_SHIFT_TO_UI:
                // console.log(`shift that is to be created in reducer is ${JSON.stringify(action.shift)}`);
                const shift = (action as IAddShiftToUiAction).payload.shift;
                if (shift.shiftParticipations == null) {
                    shift.shiftParticipations = [];
                }
                const newState = {
                    ...state,
                    shifts:
                        [
                            ...state.ui.shifts,
                            shift
                        ]
                }
                return newState as IShiftsEditUIState;
            default:
                console.log("unwanted action detected");
                console.log(JSON.stringify(action));
                //throw new Error();
                return state;
            // return state also works
        }
    }

    // create the reducer hook
    let [pageState, pageStateDispatch]: [IShiftsEditUIState, React.Dispatch<IAction>] = useReducer(reducer, initState)

    // update employees from server
    useEffect(() => {
        (async function () {
            const employees = await getEmployees(pageState.urls.serverBaseAddress);
            pageStateDispatch(setEmployeesAction(employees));

            const shiftTypes = await getShiftTypes(pageState.urls.serverBaseAddress);
            pageStateDispatch(setShiftTypesAction(shiftTypes));

            const shiftPartTypes = await getShiftParticipationTypes(pageState.urls.serverBaseAddress);
            pageStateDispatch(setShiftParticipationTypesAction(shiftPartTypes));

            const shifts = await getShiftsBetweenDates(pageState.urls.serverBaseAddress, pageState.ui.startDate, pageState.ui.endDate);
            pageStateDispatch(setShiftsAction(shifts));
        })();
    }, [pageState.urls.serverBaseAddress]);

    // created middleware to intercept dispatch calls that require async operations
    const asyncDispatch: React.Dispatch<IAction> = useCallback(async (action) => {
        switch (action.type) {
            case ActionType.GET_EMPLOYEES: {
                const employees = await getEmployees(pageState.urls.serverBaseAddress)
                pageStateDispatch(setEmployeesAction(employees));
                break;
            }
            case ActionType.GET_SHIFTS_BETWEEN_DATES: {
                const shifts = await getShiftsBetweenDates(pageState.urls.serverBaseAddress, pageState.ui.startDate, pageState.ui.endDate)
                pageStateDispatch(setShiftsAction(shifts));
                break;
            }
            case ActionType.CREATE_SHIFT_PARTICIPATION: {
                //TODO resolve this
                let shiftParticipation = (action as ICreateShiftParticipationAction).payload.shiftParticipation
                let shift = shiftParticipation.shift;
                if (shiftParticipation.shiftId == null) {
                    //create a shift
                    shift = await createShift(pageState.urls.serverBaseAddress, shiftParticipation.shift)
                    const sp = await createShiftParticipation(pageState.urls.serverBaseAddress, shiftParticipation)
                    shift.shiftParticipations.push(sp)
                    pageStateDispatch(addShiftToUiAction(shift));
                } else {
                    const sp = await createShiftParticipation(pageState.urls.serverBaseAddress, shiftParticipation)
                    pageStateDispatch(addParticipationToUiAction(sp));
                }                
                break;
            }
            default:
                pageStateDispatch(action);
        }
    }, [pageState.urls.serverBaseAddress, pageState.ui.startDate, pageState.ui.endDate]); // The empty array causes this callback to only be created once per component instance

    return [pageState, asyncDispatch];
}