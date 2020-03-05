import { ActionType } from "./ActionType";
export function deleteShiftAction(shift) {
    return {
        type: ActionType.DELETE_SHIFT,
        payload: { shift }
    };
}
//# sourceMappingURL=DeleteShiftAction.js.map