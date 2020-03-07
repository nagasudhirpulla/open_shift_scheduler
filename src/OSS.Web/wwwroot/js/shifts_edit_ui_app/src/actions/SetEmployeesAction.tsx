import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IEmployee } from "../type_defs/IEmployee";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface ISetEmployeesPayload {
    employees: IEmployee[]
}

export interface ISetEmployeesAction extends IAction {
    type: ActionType.SET_EMPLOYEES,
    payload: ISetEmployeesPayload
}

export function setEmployeesAction(employees: IEmployee[]): ISetEmployeesAction {
    return {
        type: ActionType.SET_EMPLOYEES,
        payload: { employees }
    };
}

export const setEmployeesReducer = (state: IShiftsEditUIState, action: ISetEmployeesAction): IShiftsEditUIState => {
    return {
        ...state,
        ui: {
            ...state.ui,
            employees: action.payload.employees
        }
    } as IShiftsEditUIState;
}