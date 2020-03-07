import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipationType } from "../type_defs/IShiftParticipationType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

export interface ISetShiftParticipationTypesPayload {
    shiftParticipationTypes: IShiftParticipationType[]
}

export interface ISetShiftParticipationTypesAction extends IAction {
    type: ActionType.SET_SHIFT_PARTICIPATION_TYPES,
    payload: ISetShiftParticipationTypesPayload
}

export function setShiftParticipationTypesAction(shiftParticipationTypes: IShiftParticipationType[]): ISetShiftParticipationTypesAction {
    return {
        type: ActionType.SET_SHIFT_PARTICIPATION_TYPES,
        payload: { shiftParticipationTypes }
    };
}

export const setShiftParticipationTypesReducer = (state: IShiftsEditUIState, action: ISetShiftParticipationTypesAction): IShiftsEditUIState => {
    return {
        ...state,
        ui: {
            ...state.ui,
            shiftParticipationTypes: action.payload.shiftParticipationTypes
        }
    } as IShiftsEditUIState;
}