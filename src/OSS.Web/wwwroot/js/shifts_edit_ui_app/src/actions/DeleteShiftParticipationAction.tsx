import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { deleteShiftParticipation } from "../server_mediators/shiftParticipation";
import { deleteParticipationFromUiAction } from "./deleteParticipationFromUiAction";

export interface IDeleteShiftParticipationPayload {
    shiftParticipation: IShiftParticipation
}

export interface IDeleteShiftParticipationAction extends IAction {
    type: ActionType.DELETE_SHIFT_PARTICIPATION,
    payload: IDeleteShiftParticipationPayload
}

export function deleteShiftParticipationAction(shiftParticipation: IShiftParticipation): IDeleteShiftParticipationAction {
    return {
        type: ActionType.DELETE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation }
    };
}

export const deleteShiftParticipationDispatch = async (action: IDeleteShiftParticipationAction, pageState: IShiftsEditUIState, pageStateDispatch: React.Dispatch<IAction>): Promise<void> => {
    const sp = await deleteShiftParticipation(pageState.urls.serverBaseAddress, action.payload.shiftParticipation)
    pageStateDispatch(deleteParticipationFromUiAction(sp));
}