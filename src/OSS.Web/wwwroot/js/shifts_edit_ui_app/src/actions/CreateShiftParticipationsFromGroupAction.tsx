import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { createShiftParticipationsFromShiftGroup } from "../server_mediators/shiftParticipation";
import { setShiftParticipationsAction } from "./setShiftParticipationsAction";
import { IShift } from "../type_defs/IShift";
import { createShift } from "../server_mediators/shifts";
import { addShiftToUiAction } from "./AddShiftToUiAction";

export interface ICreateShiftParticipationsFromGroupPayload {
    shift: IShift,
    shiftGroupId: number
}

export interface ICreateShiftParticipationsFromGroupAction extends IAction {
    type: ActionType.CREATE_SHIFT_PARTICIPATIONS_FROM_GROUP,
    payload: ICreateShiftParticipationsFromGroupPayload
}

export function createShiftParticipationsFromGroupAction(shift: IShift, shiftGroupId: number): ICreateShiftParticipationsFromGroupAction {
    return {
        type: ActionType.CREATE_SHIFT_PARTICIPATIONS_FROM_GROUP,
        payload: { shift, shiftGroupId }
    };
}

export const createShiftParticipationsFromGroupDispatch = async (action: ICreateShiftParticipationsFromGroupAction, pageState: IShiftsEditUIState, pageStateDispatch: React.Dispatch<IAction>): Promise<void> => {
    let shift = action.payload.shift;
    if (shift.id == null) {
        //create a shift since it was not present
        shift = await createShift(pageState.urls.serverBaseAddress, shift)
        const shiftParticipations = await createShiftParticipationsFromShiftGroup(pageState.urls.serverBaseAddress, action.payload.shiftGroupId, shift.id)
        shift.shiftParticipations = shiftParticipations
        pageStateDispatch(addShiftToUiAction(shift));
    } else {
        const shiftParticipations = await createShiftParticipationsFromShiftGroup(pageState.urls.serverBaseAddress, action.payload.shiftGroupId, action.payload.shift.id)
        pageStateDispatch(setShiftParticipationsAction(shiftParticipations));
    }
}