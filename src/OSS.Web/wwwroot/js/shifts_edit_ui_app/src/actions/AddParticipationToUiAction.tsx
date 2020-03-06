import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

export interface IAddParticipationToUiPayload {
    shiftParticipation: IShiftParticipation
}

export interface IAddParticipationToUiAction extends IAction {
    type: ActionType.ADD_PARTICIPATION_TO_UI,
    payload: IAddParticipationToUiPayload
}

export function addParticipationToUiAction(shiftParticipation: IShiftParticipation): IAddParticipationToUiAction {
    return {
        type: ActionType.ADD_PARTICIPATION_TO_UI,
        payload: { shiftParticipation }
    };
}