import { ActionType } from "./ActionType";
export function getShiftsBetweenDatesAction(startDate, endDate) {
    return {
        type: ActionType.GET_SHIFTS_BETWEEN_DATES,
        payload: { startDate, endDate }
    };
}
//# sourceMappingURL=GetShiftsBetweenDatesAction.js.map