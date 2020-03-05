import { ActionType } from "../actions/ActionType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { IAction } from "../type_defs/IAction";
import { ISetEmployeesAction, setEmployeesAction } from "../actions/SetEmployeesAction";
import { useReducer, useEffect, useCallback } from "react";
import { getEmployees } from "../server_mediators/employees";
import { ISetStartTimeAction } from "../actions/SetStartTimeAction";
import { ISetEndTimeAction } from "../actions/SetEndTimeAction";
import { getShiftsBetweenDates } from "../server_mediators/shifts";
import { setShiftsAction, ISetShiftsAction } from "../actions/SetShiftsAction";

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
            default:
                console.log("unwanted action detected");
                console.log(JSON.stringify(action));
                throw new Error();
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
            default:
                pageStateDispatch(action);
        }
    }, [pageState.urls.serverBaseAddress, pageState.ui.startDate, pageState.ui.endDate]); // The empty array causes this callback to only be created once per component instance

    return [pageState, asyncDispatch];
}