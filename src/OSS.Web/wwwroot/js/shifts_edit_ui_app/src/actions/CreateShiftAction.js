import { ActionType } from "./ActionType";
export function createShiftAction(shift) {
    return {
        type: ActionType.CREATE_SHIFT,
        payload: { shift }
    };
}
//# sourceMappingURL=CreateShiftAction.js.map