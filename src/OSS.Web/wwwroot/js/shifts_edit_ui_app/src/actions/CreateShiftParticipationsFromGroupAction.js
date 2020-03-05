import { ActionType } from "./ActionType";
export function getShiftTypesAction(shiftId, shiftGroupId) {
    return {
        type: ActionType.CREATE_SHIFT_PARTICIPATIONS_FROM_GROUP,
        payload: { shiftId, shiftGroupId }
    };
}
//# sourceMappingURL=CreateShiftParticipationsFromGroupAction.js.map