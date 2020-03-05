import { ActionType } from "./ActionType";
export function deleteShiftParticipationAction(shiftParticipation) {
    return {
        type: ActionType.DELETE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation }
    };
}
//# sourceMappingURL=DeleteShiftParticipationAction.js.map