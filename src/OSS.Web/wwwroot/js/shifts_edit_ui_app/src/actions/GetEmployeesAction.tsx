import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";

export interface IGetEmployeesPayload {

}

export interface IGetEmployeesAction extends IAction {
    type: ActionType.GET_EMPLOYEES,
    payload: IGetEmployeesPayload
}

export function getEmployeesAction(): IGetEmployeesAction {
    return {
        type: ActionType.GET_EMPLOYEES,
        payload: {}
    };
}