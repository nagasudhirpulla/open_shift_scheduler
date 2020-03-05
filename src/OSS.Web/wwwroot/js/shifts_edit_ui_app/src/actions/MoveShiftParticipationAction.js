import { ActionType } from "./ActionType";
export function getEmployeesAction(shiftParticipation, direction) {
    return {
        type: ActionType.MOVE_SHIFT_PARTICIPATION,
        payload: { shiftParticipation, direction }
    };
}
//# sourceMappingURL=MoveShiftParticipationAction.js.map