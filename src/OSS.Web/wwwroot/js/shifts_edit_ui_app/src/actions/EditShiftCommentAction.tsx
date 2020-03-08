import { IAction } from "../type_defs/IAction";
import { ActionType } from "./ActionType";
import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";
import { editShiftComment, createShift } from "../server_mediators/shifts";
import { updateShiftCommentsAction } from "./UpdateShiftCommentsAction";
import { IShift } from "../type_defs/IShift";
import { addShiftToUiAction } from "./AddShiftToUiAction";

export interface IEditShiftCommentPayload {
    shift: IShift,
    comment: string
}

export interface IEditShiftCommentAction extends IAction {
    type: ActionType.EDIT_SHIFT_COMMENT,
    payload: IEditShiftCommentPayload
}

export function editShiftCommentAction(shift: IShift, comment: string): IEditShiftCommentAction {
    return {
        type: ActionType.EDIT_SHIFT_COMMENT,
        payload: { shift, comment }
    };
}

export const editShiftCommentDispatch = async (action: IEditShiftCommentAction, pageState: IShiftsEditUIState, pageStateDispatch: React.Dispatch<IAction>): Promise<void> => {
    let shift = { ... action.payload.shift }
    if (shift.id == null) {
        //create a shift since it was not present
        shift = await createShift(pageState.urls.serverBaseAddress, shift)
        pageStateDispatch(addShiftToUiAction(shift));
    }
    const isSuccess = await editShiftComment(pageState.urls.serverBaseAddress, action.payload.comment, shift)
    if (isSuccess) {
        pageStateDispatch(updateShiftCommentsAction(shift.id, action.payload.comment));
    }
}