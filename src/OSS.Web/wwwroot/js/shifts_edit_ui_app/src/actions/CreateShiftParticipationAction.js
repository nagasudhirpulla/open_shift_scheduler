import { ActionType } from "./ActionType";
export function createShiftParticipationAction(shiftParticipation) {
    return {
        type: ActionType.CREATE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation }
    };
}
//# sourceMappingURL=CreateShiftParticipationAction.js.map