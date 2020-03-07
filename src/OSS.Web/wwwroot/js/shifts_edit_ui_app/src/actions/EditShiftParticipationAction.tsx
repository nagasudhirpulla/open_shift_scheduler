import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { editShiftParticipation } from "../server_mediators/shiftParticipation";
import { updateShiftParticipationAction } from "./UpdateShiftParticipationAction";

export interface IEditShiftParticipationPayload {
    shiftParticipation: IShiftParticipation
}

export interface IEditShiftParticipationAction extends IAction {
    type: ActionType.EDIT_SHIFT_PARTICIPATION,
    payload: IEditShiftParticipationPayload
}

export function editShiftParticipationAction(shiftParticipation: IShiftParticipation): IEditShiftParticipationAction {
    return {
        type: ActionType.EDIT_SHIFT_PARTICIPATION,
        payload: { shiftParticipation: shiftParticipation }
    };
}

export const editShiftParticipationDispatch = async (action: IEditShiftParticipationAction, pageState: IShiftsEditUIState, pageStateDispatch: React.Dispatch<IAction>): Promise<void> => {
    let shiftParticipation = action.payload.shiftParticipation
    const isSuccess = await editShiftParticipation(pageState.urls.serverBaseAddress, shiftParticipation)
    if (isSuccess) {
        pageStateDispatch(updateShiftParticipationAction(shiftParticipation));
    }
}