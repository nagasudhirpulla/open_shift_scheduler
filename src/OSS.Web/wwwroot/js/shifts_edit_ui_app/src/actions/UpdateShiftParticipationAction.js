import { ActionType } from "./ActionType";
export function updateShiftParticipationAction(shiftParticipation) {
    return {
        type: ActionType.UPDATE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation: shiftParticipation }
    };
}
//# sourceMappingURL=UpdateShiftParticipationAction.js.map