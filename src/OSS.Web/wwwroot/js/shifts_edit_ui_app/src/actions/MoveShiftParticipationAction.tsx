import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { moveShiftParticipation } from "../server_mediators/shiftParticipation";
import { updateShiftAction } from "./UpdateShiftAction";

export interface IMoveShiftParticipationPayload {
    shiftParticipation: IShiftParticipation,
    direction: 1 | -1
}

export interface IMoveShiftParticipationAction extends IAction {
    type: ActionType.MOVE_SHIFT_PARTICIPATION,
    payload: IMoveShiftParticipationPayload
}

export function moveShiftParticipationAction(shiftParticipation: IShiftParticipation, direction: 1 | -1): IMoveShiftParticipationAction {
    return {
        type: ActionType.MOVE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation, direction }
    };
}

export const moveShiftParticipationDispatch = async (action: IMoveShiftParticipationAction, pageState: IShiftsEditUIState, pageStateDispatch: React.Dispatch<IAction>): Promise<void> => {
    let shiftParticipation = action.payload.shiftParticipation
    let direction = action.payload.direction;
    let shift = await moveShiftParticipation(pageState.urls.serverBaseAddress, shiftParticipation.id, direction)
    pageStateDispatch(updateShiftAction(shift))
}