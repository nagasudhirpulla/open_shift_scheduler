/*
https://github.com/SophieDeBenedetto/catbook-redux/blob/master/src/reducers/initialState.js
*/
export default {
    shifts_ui: {
        server_base_addr: 'http://localhost:8807',
        shifts: [],
        employees: [],
        shift_types: [],
        shift_groups: [],
        shift_skills: []
    },
    shifts_ui_cell: {
        shift: {
            "shiftId": null,
            "shiftType": null,
            "shiftTypeId": 0,
            "shiftDate": null,
            "shiftParticipations": []
        },
        shift_type: {
            "shiftTypeId": 1,
            "name": "Morning",
            "startOffsetHrs": 8,
            "startOffsetMins": 0,
            "roasterSequence": 1,
            "shiftSequence": 1,
            "displayColor": "0, 255, 230",
            "colorString": "#00FFE6"
        },
        'employees_dict':{},
        col_size: 3
    }
}