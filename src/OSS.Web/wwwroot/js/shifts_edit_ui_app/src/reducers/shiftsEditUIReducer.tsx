import { ActionType } from "../actions/ActionType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { IAction } from "../type_defs/IAction";
import { ISetEmployeesAction, setEmployeesAction, setEmployeesReducer } from "../actions/SetEmployeesAction";
import { useReducer, useEffect, useCallback } from "react";
import { getEmployees } from "../server_mediators/employees";
import { ISetStartTimeAction, setStartTimeReducer } from "../actions/SetStartTimeAction";
import { ISetEndTimeAction, setEndTimeReducer } from "../actions/SetEndTimeAction";
import { getShiftsBetweenDates } from "../server_mediators/shifts";
import { setShiftsAction, ISetShiftsAction, setShiftsReducer } from "../actions/SetShiftsAction";
import { getShiftTypes } from "../server_mediators/shiftTypes";
import { setShiftTypesAction, ISetShiftTypesAction, setShiftTypesReducer } from "../actions/SetShiftTypesAction";
import { getShiftParticipationTypes } from "../server_mediators/shiftParticipationTypes";
import { setShiftParticipationTypesAction, ISetShiftParticipationTypesAction, setShiftParticipationTypesReducer } from "../actions/SetShiftParticipationTypesAction";
import { ISetActiveShiftAction, setActiveShiftReducer } from "../actions/SetActiveShiftAction";
import { ICreateShiftParticipationAction, createShiftParticipationDispatch } from "../actions/CreateShiftParticipationAction";
import { IAddParticipationToUiAction, addParticipationToUiReducer } from "../actions/AddParticipationToUiAction";
import { addShiftToUiAction, IAddShiftToUiAction, addShiftToUiReducer } from "../actions/AddShiftToUiAction";
import { getShiftsForUiAction } from "../actions/GetShiftsForUiAction";
import { getEmployeesAction } from "../actions/GetEmployeesAction";
import { deleteShiftParticipationDispatch, IDeleteShiftParticipationAction } from "../actions/DeleteShiftParticipationAction";
import { IDeleteParticipationFromUiAction, deleteParticipationFromUiReducer } from "../actions/deleteParticipationFromUiAction";
import { ISetShiftParticipationsAction, setShiftParticipationsReducer } from "../actions/setShiftParticipationsAction";
import { createShiftParticipationsFromGroupDispatch, ICreateShiftParticipationsFromGroupAction } from "../actions/CreateShiftParticipationsFromGroupAction";
import { setShiftGroupsReducer, ISetShiftGroupsAction, setShiftGroupsAction } from "../actions/SetShiftGroupsAction";
import { getShiftGroups } from "../server_mediators/shiftGroups";
import { editShiftParticipation } from "../server_mediators/shiftParticipation";
import { editShiftParticipationDispatch, IEditShiftParticipationAction } from "../actions/EditShiftParticipationAction";
import { updateShiftParticipationReducer, IUpdateShiftParticipationAction } from "../actions/UpdateShiftParticipationAction";
import { setActiveShiftParticipationReducer, ISetActiveShiftParticipationAction } from "../actions/SetActiveShiftParticipationAction";

export const useShiftsEditUIReducer = (initState: IShiftsEditUIState): [IShiftsEditUIState, React.Dispatch<IAction>] => {
    // create the reducer function
    const reducer = (state: IShiftsEditUIState, action: IAction): IShiftsEditUIState => {
        switch (action.type) {
            case ActionType.SET_START_TIME:
                return setStartTimeReducer(state, action as ISetStartTimeAction)
            case ActionType.SET_END_TIME:
                return setEndTimeReducer(state, action as ISetEndTimeAction)
            case ActionType.SET_EMPLOYEES:
                return setEmployeesReducer(state, action as ISetEmployeesAction)
            case ActionType.SET_SHIFT_TYPES:
                return setShiftTypesReducer(state, action as ISetShiftTypesAction)
            case ActionType.SET_SHIFT_PARTICIPATION_TYPES:
                return setShiftParticipationTypesReducer(state, action as ISetShiftParticipationTypesAction)
            case ActionType.SET_SHIFT_GROUPS:
                return setShiftGroupsReducer(state, action as ISetShiftGroupsAction)
            case ActionType.SET_SHIFTS:
                return setShiftsReducer(state, action as ISetShiftsAction)
            case ActionType.SET_ACTIVE_SHIFT:
                return setActiveShiftReducer(state, action as ISetActiveShiftAction)
            case ActionType.SET_ACTIVE_SHIFT_PARTICIPATION:
                return setActiveShiftParticipationReducer(state, action as ISetActiveShiftParticipationAction)
            case ActionType.ADD_SHIFT_TO_UI:
                return addShiftToUiReducer(state, action as IAddShiftToUiAction)
            case ActionType.ADD_PARTICIPATION_TO_UI:
                return addParticipationToUiReducer(state, action as IAddParticipationToUiAction)
            case ActionType.DELETE_SHIFT_PARTICIPATION_FROM_UI:
                return deleteParticipationFromUiReducer(state, action as IDeleteParticipationFromUiAction)
            case ActionType.SET_SHIFT_PARTICIPATIONS:
                return setShiftParticipationsReducer(state, action as ISetShiftParticipationsAction)
            case ActionType.UPDATE_SHIFT_PARTICIPATION:
                return updateShiftParticipationReducer(state, action as IUpdateShiftParticipationAction)
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
            asyncDispatch(getEmployeesAction())

            const shiftTypes = await getShiftTypes(pageState.urls.serverBaseAddress);
            pageStateDispatch(setShiftTypesAction(shiftTypes));

            const shiftPartTypes = await getShiftParticipationTypes(pageState.urls.serverBaseAddress);
            pageStateDispatch(setShiftParticipationTypesAction(shiftPartTypes));

            const shiftGroups = await getShiftGroups(pageState.urls.serverBaseAddress);
            pageStateDispatch(setShiftGroupsAction(shiftGroups));

            asyncDispatch(getShiftsForUiAction())
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
            case ActionType.GET_SHIFTS_FOR_UI: {
                const shifts = await getShiftsBetweenDates(pageState.urls.serverBaseAddress, pageState.ui.startDate, pageState.ui.endDate)
                pageStateDispatch(setShiftsAction(shifts));
                break;
            }
            case ActionType.CREATE_SHIFT_PARTICIPATION: {
                await createShiftParticipationDispatch(action as ICreateShiftParticipationAction, pageState, pageStateDispatch)
                break;
            }
            case ActionType.DELETE_SHIFT_PARTICIPATION: {
                await deleteShiftParticipationDispatch(action as IDeleteShiftParticipationAction, pageState, pageStateDispatch)
                break;
            }
            case ActionType.CREATE_SHIFT_PARTICIPATIONS_FROM_GROUP: {
                await createShiftParticipationsFromGroupDispatch(action as ICreateShiftParticipationsFromGroupAction, pageState, pageStateDispatch)
                break;
            }
            case ActionType.EDIT_SHIFT_PARTICIPATION: {
                await editShiftParticipationDispatch(action as IEditShiftParticipationAction, pageState, pageStateDispatch)
                break;
            }
            default:
                pageStateDispatch(action);
        }
    }, [pageState.urls.serverBaseAddress, pageState.ui.startDate, pageState.ui.endDate]); // The empty array causes this callback to only be created once per component instance

    return [pageState, asyncDispatch];
}