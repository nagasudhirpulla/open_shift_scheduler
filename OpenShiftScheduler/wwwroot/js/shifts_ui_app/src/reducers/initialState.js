/*
https://github.com/SophieDeBenedetto/catbook-redux/blob/master/src/reducers/initialState.js
*/
export default {
    shifts_ui: {
        server_base_addr: '.',
        shifts: [
            {
                "shiftId": 3,
                "shiftType": null,
                "shiftTypeId": 2,
                "shiftDate": "2019-04-15T00:00:00",
                "comments": "abc",
                "shiftParticipations": [
                    {
                        "shiftParticipationId": 1,
                        "shiftParticipationTypeId": null,
                        "employee": null,
                        "employeeId": 1,
                        "shiftId": 3,
                        "participationSequence": 0
                    }
                ]
            },
            {
                "shiftId": 4,
                "shiftType": null,
                "shiftTypeId": 3,
                "shiftDate": "2019-04-15T00:00:00",
                "shiftParticipations": [],
                "comments": "def"
            }
        ],
        employees: [
            {
                "employeeId": 2,
                "officeId": 300043,
                "gender": null,
                "genderId": 1,
                "name": "Mastanvali",
                "phone": null,
                "email": "mastanvali@posoco.in",
                "dob": "1992-03-03T00:00:00",
                "isActive": true,
                "shiftRole": null,
                "shiftRoleId": 2,
                "shiftGroup": null,
                "shiftGroupId": 1,
                "employeeShiftSkills": [
                    {
                        "employeeShiftSkillId": 2,
                        "employeeId": 2,
                        "shiftSkill": null,
                        "shiftSkillId": 1
                    },
                    {
                        "employeeShiftSkillId": 4,
                        "employeeId": 2,
                        "shiftSkill": null,
                        "shiftSkillId": 2
                    }
                ],
                "shiftParticipations": null
            },
            {
                "employeeId": 1,
                "officeId": 30044,
                "gender": null,
                "genderId": 1,
                "name": "Nagasudhir",
                "phone": null,
                "email": "nagasudhir@posoco.in",
                "dob": "1992-03-03T00:00:00",
                "isActive": true,
                "shiftRole": null,
                "shiftRoleId": 2,
                "shiftGroup": null,
                "shiftGroupId": 1,
                "employeeShiftSkills": [
                    {
                        "employeeShiftSkillId": 1,
                        "employeeId": 1,
                        "shiftSkill": null,
                        "shiftSkillId": 2
                    }
                ],
                "shiftParticipations": null
            }
        ],
        shift_types: [
            {
                "shiftTypeId": 1,
                "name": "Morning",
                "startOffsetHrs": 8,
                "startOffsetMins": 0,
                "roasterSequence": 1,
                "shiftSequence": 1,
                "displayColor": "0, 255, 230",
                "colorString": "#00FFE6"
            },
            {
                "shiftTypeId": 2,
                "name": "Evening",
                "startOffsetHrs": 14,
                "startOffsetMins": 0,
                "roasterSequence": 2,
                "shiftSequence": 2,
                "displayColor": "255, 242, 0",
                "colorString": "#FFF200"
            },
            {
                "shiftTypeId": 3,
                "name": "Night",
                "startOffsetHrs": 21,
                "startOffsetMins": 0,
                "roasterSequence": 3,
                "shiftSequence": 3,
                "displayColor": "149, 0, 255",
                "colorString": "#9500FF"
            }
        ],
        shift_groups: [
            {
                "shiftGroupId": 1,
                "name": "A Group",
                "employees": [
                    {
                        "employeeId": 1,
                        "officeId": 30044,
                        "gender": null,
                        "genderId": 1,
                        "name": "Nagasudhir",
                        "phone": null,
                        "email": "nagasudhir@posoco.in",
                        "dob": "1992-03-03T00:00:00",
                        "isActive": true,
                        "shiftRole": null,
                        "shiftRoleId": 2,
                        "shiftGroupId": 1,
                        "employeeShiftSkills": null,
                        "shiftParticipations": null
                    },
                    {
                        "employeeId": 2,
                        "officeId": 300043,
                        "gender": null,
                        "genderId": 1,
                        "name": "Mastanvali",
                        "phone": null,
                        "email": "mastanvali@posoco.in",
                        "dob": "1992-03-03T00:00:00",
                        "isActive": true,
                        "shiftRole": null,
                        "shiftRoleId": 2,
                        "shiftGroupId": 1,
                        "employeeShiftSkills": null,
                        "shiftParticipations": null
                    }
                ]
            }
        ],
        shift_skills: [
            {
                "shiftSkillId": 1,
                "name": "Outage Coordination",
                "employeeShiftSkills": null
            },
            {
                "shiftSkillId": 2,
                "name": "Scheduling",
                "employeeShiftSkills": null
            },
            {
                "shiftSkillId": 3,
                "name": "RTSD",
                "employeeShiftSkills": null
            }
        ],
        shift_participation_types: [
            {
                "shiftParticipationTypeId": 1,
                "name": "Normal",
                "isAbsence": false
            },
            {
                "shiftParticipationTypeId": 2,
                "name": "Leave",
                "isAbsence": true
            },
            {
                "shiftParticipationTypeId": 3,
                "name": "Training",
                "isAbsence": true
            },
            {
                "shiftParticipationTypeId": 4,
                "name": "From General",
                "isAbsence": false
            }
        ]
    }
}