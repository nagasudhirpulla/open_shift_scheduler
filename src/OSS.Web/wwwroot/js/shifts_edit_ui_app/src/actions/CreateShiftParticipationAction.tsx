import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { createShift } from "../server_mediators/shifts";
import { addShiftToUiAction } from "./AddShiftToUiAction";
import { addParticipationToUiAction } from "./AddParticipationToUiAction";
import { createShiftParticipation } from "../server_mediators/shiftParticipation";

export interface ICreateShiftParticipationPayload {
    shiftParticipation: IShiftParticipation
}

export interface ICreateShiftParticipationAction extends IAction {
    type: ActionType.CREATE_SHIFT_PARTICIPATION,
    payload: ICreateShiftParticipationPayload
}

export function createShiftParticipationAction(shiftParticipation: IShiftParticipation): ICreateShiftParticipationAction {
    return {
        type: ActionType.CREATE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation }
    };
}

export const createShiftParticipationDispatch = async (action: ICreateShiftParticipationAction, pageState: IShiftsEditUIState, pageStateDispatch: React.Dispatch<IAction>): Promise<void> => {
    let shiftParticipation = action.payload.shiftParticipation
    let shift = shiftParticipation.shift;
    if (shiftParticipation.shiftId == null) {
        //create a shift since it was not present
        shift = await createShift(pageState.urls.serverBaseAddress, shiftParticipation.shift)
        // now set the shift Id from the newly created shift
        shiftParticipation.shiftId = shift.id
        const sp = await createShiftParticipation(pageState.urls.serverBaseAddress, shiftParticipation)
        shift.shiftParticipations.push(sp)
        pageStateDispatch(addShiftToUiAction(shift));
    } else {
        const sp = await createShiftParticipation(pageState.urls.serverBaseAddress, shiftParticipation)
        pageStateDispatch(addParticipationToUiAction(sp));
    }
}