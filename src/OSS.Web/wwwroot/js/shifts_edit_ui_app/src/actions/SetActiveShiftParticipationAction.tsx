import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShift } from "../type_defs/IShift";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

export interface ISetActiveShiftParticipationPayload {
    shiftParticipation: IShiftParticipation
}

export interface ISetActiveShiftParticipationAction extends IAction {
    type: ActionType.SET_ACTIVE_SHIFT_PARTICIPATION,
    payload: ISetActiveShiftParticipationPayload
}

export function setActiveShiftParticipationAction(shiftParticipation: IShiftParticipation): ISetActiveShiftParticipationAction {
    return {
        type: ActionType.SET_ACTIVE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation }
    };
}

export const setActiveShiftParticipationReducer = (state: IShiftsEditUIState, action: ISetActiveShiftParticipationAction): IShiftsEditUIState => {
    return {
        ...state,
        ui: {
            ...state.ui,
            activeShiftParticipation: action.payload.shiftParticipation
        }
    } as IShiftsEditUIState;
}