import React from 'react';
import DateTime from 'react-datetime';
// https://react-bootstrap.github.io/components/modal/
import pageInitState from '../initial_states/shiftEditUIInitState';
import { useShiftsEditUIReducer } from '../reducers/shiftsEditUIReducer';
import moment from 'moment';
import { setStartTimeAction } from '../actions/SetStartTimeAction';
import { setEndTimeAction } from '../actions/SetEndTimeAction';
import { getShiftsBetweenDatesAction } from '../actions/GetShiftsBetweenDatesAction';
import { dateToKeyString } from '../utils/timeUtils';
import { groupObjBy, convertShiftsToMatrix } from '../utils/objUtils'
import { IShift } from '../type_defs/IShift';
import { IShiftType } from '../type_defs/IShiftType';
import { IShiftParticipationType } from '../type_defs/IShiftParticipationType';
import { IGroupedShiftType } from '../type_defs/IGroupedShiftType';
import { IGetShiftGroupsPayload } from '../actions/GetShiftGroupsAction';
import { IGroupedShiftParticipationType } from '../type_defs/IGroupedShiftParticipationType';
import { IGroupedEmployee } from '../type_defs/IGroupedEmployee';

function ShiftsEditApp() {
    let [pageState, pageStateDispatch] = useShiftsEditUIReducer(pageInitState);
    const onStartTimeChanged = (timeObj) => {
        if (timeObj instanceof moment) {
            let dateObj = moment(timeObj).toDate();
            pageStateDispatch(setStartTimeAction(dateObj))
        }
    }
    const onEndTimeChanged = (timeObj) => {
        if (timeObj instanceof moment) {
            let dateObj = moment(timeObj).toDate();
            pageStateDispatch(setEndTimeAction(dateObj))
        }
    }
    const onLoadShiftsClick = () => {
        pageStateDispatch(getShiftsBetweenDatesAction())
    }
    const shiftMatrix = convertShiftsToMatrix(pageState.ui.startDate, pageState.ui.endDate, pageState.ui.shifts, pageState.ui.shiftTypes);

    // group the shift types by shiftTypeId
    let groupedEmployees = groupObjBy(pageState.ui.shiftTypes, 'shiftTypeId') as IGroupedEmployee;
    let groupedShiftTypes = groupObjBy(pageState.ui.shiftTypes, 'shiftTypeId') as IGroupedShiftType;
    let groupedShiftPartTypes = groupObjBy(pageState.ui.shiftParticipationTypes, 'shiftParticipationTypeId') as IGroupedShiftParticipationType;

    return (
        <>
            <h3>Edit Shifts</h3>
            <div className={"datePickerDiv"}>
                <span>Start Time{" "}</span>
                <DateTime
                    value={pageState.ui.startDate}
                    dateFormat={'DD-MM-YYYY'}
                    timeFormat={false}
                    onChange={onStartTimeChanged}
                    className={"timePicker"}
                />
            </div>
            <div style={{ marginLeft: "0.5em" }} className={"datePickerDiv"}>
                <span>End Time{"  "}</span>
                <DateTime
                    value={pageState.ui.endDate}
                    dateFormat={'DD-MM-YYYY'}
                    timeFormat={false}
                    onChange={onEndTimeChanged}
                    className={"timePicker"}
                />
            </div>
            <button onClick={onLoadShiftsClick} className={"btn btn-success btn-sm btn-icon-split loadBtn"}>
                <span className={"icon text-white-50"}>
                    <i className={"fas fa-sync"}></i>
                </span>
                <span className={"text"}>Load</span>
            </button>
            <br />
            <pre>{JSON.stringify(pageState, null, 2)}</pre>
        </>
    );
}
export default ShiftsEditApp;