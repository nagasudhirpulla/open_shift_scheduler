import { IShiftsEditUIState } from "../type_defs/IShiftsEditUIState";

const nowDate: Date = new Date()
const startDate: Date = new Date(nowDate.getFullYear(), nowDate.getMonth(), 1)
const endDate = new Date(nowDate.setMonth(nowDate.getMonth() + 1))

const initState: IShiftsEditUIState = {
    ui: {
        shifts: [],
        employees: [],
        shiftTypes: [],
        shiftGroups: [],
        shiftParticipation_types: [],
        startDate: startDate,
        endDate: endDate
    },
    urls: {
        serverBaseAddress: ".."
    }
}

export default initState;