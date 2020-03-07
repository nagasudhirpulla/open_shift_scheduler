import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

const nowDate: Date = new Date()
const startDate: Date = new Date(nowDate.getFullYear(), nowDate.getMonth(), 1)
let endDate: Date = new Date(startDate.getTime())
endDate = new Date(endDate.setMonth(endDate.getMonth() + 1))

const initState: IShiftsEditUIState = {
    ui: {
        shifts: [],
        employees: [],
        shiftTypes: [],
        shiftGroups: [],
        shiftParticipationTypes: [],
        startDate: startDate,
        endDate: endDate,
        activeShift: null,
        activeShiftParticipation: null
    },
    urls: {
        serverBaseAddress: ".."
    }
}

export default initState;