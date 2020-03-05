import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IEmployee } from "../type_defs/IEmployee";

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