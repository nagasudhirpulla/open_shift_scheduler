import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

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