// https://react-bootstrap.github.io/components/modal/
import React, { useState } from 'react';
import DateTime from 'react-datetime';
import pageInitState from '../initial_states/shiftEditUIInitState';
import { useShiftsEditUIReducer } from '../reducers/shiftsEditUIReducer';
import moment from 'moment';
import { setStartTimeAction } from '../actions/SetStartTimeAction';
import { setEndTimeAction } from '../actions/SetEndTimeAction';
import { getShiftsBetweenDatesAction } from '../actions/GetShiftsBetweenDatesAction';
import ShiftCellsMatrix from './ShiftCellsMatrix';
import CommentsElement from './CommentsElement';
import { IShift } from '../type_defs/IShift';
import { updateShiftCommentsAction } from '../actions/UpdateShiftCommentsAction';
import { IShiftParticipation } from '../type_defs/IShiftParticipation';
import { moveShiftParticipationAction } from '../actions/MoveShiftParticipationAction';
import { updateShiftParticipationAction } from '../actions/UpdateShiftParticipationAction';
import { deleteShiftParticipationAction } from '../actions/DeleteShiftParticipationAction';
import { deleteShiftAction } from '../actions/DeleteShiftAction';
import AddEmployeeModal from './AddEmployeeModal';
import { createShiftParticipationAction } from '../actions/CreateShiftParticipationAction';
import { setActiveShiftAction } from '../actions/SetActiveShiftAction';

function ShiftsEditApp() {
    let [pageState, pageStateDispatch] = useShiftsEditUIReducer(pageInitState);
    const [showAddEmpModal, setAddEmpModalShow] = useState(false);

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
    //console.log(pageState.ui.shiftTypes)
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
            <button onClick={() => { pageStateDispatch(getShiftsBetweenDatesAction()) }} className={"btn btn-success btn-sm btn-icon-split loadBtn"}>
                <span className={"icon text-white-50"}>
                    <i className={"fas fa-sync"}></i>
                </span>
                <span className={"text"}>Load</span>
            </button>
            <br />
            <br />
            <ShiftCellsMatrix
                startDate={pageState.ui.startDate}
                endDate={pageState.ui.endDate}
                shifts={pageState.ui.shifts}
                shiftTypes={pageState.ui.shiftTypes}
                employees={pageState.ui.employees}
                shiftParticipationTypes={pageState.ui.shiftParticipationTypes}
                editShiftComments={(shift: IShift) => { pageStateDispatch(updateShiftCommentsAction(shift)) }}
                moveShiftParticipation={(sp: IShiftParticipation, dir: -1 | 1) => { pageStateDispatch(moveShiftParticipationAction(sp, dir)) }}
                updateShiftParticipation={(sp: IShiftParticipation) => { pageStateDispatch(updateShiftParticipationAction(sp)) }}
                removeShiftParticipation={(sp: IShiftParticipation) => { pageStateDispatch(deleteShiftParticipationAction(sp)) }}
                createShiftParticipation={(s: IShift) => { pageStateDispatch(setActiveShiftAction(s)); setAddEmpModalShow(true); }}
                createShiftParticipationFromGroup={(s: IShift) => { /*TODO show modal here*/ }}
                removeAllShiftParticipations={(s: IShift) => { pageStateDispatch(deleteShiftAction(s)) }}
            />
            <br />
            <CommentsElement
                shifts={pageState.ui.shifts}
                shiftTypes={pageState.ui.shiftTypes}
                editShiftComments={(shift: IShift) => { pageStateDispatch(updateShiftCommentsAction(shift)) }}
            />
            <AddEmployeeModal
                show={showAddEmpModal}
                setShow={setAddEmpModalShow}
                shift={pageState.ui.activeShift}
                employees={pageState.ui.employees}
                shiftParticipationTypes={pageState.ui.shiftParticipationTypes}
                onParticipationSubmit={(sp: IShiftParticipation) => { pageStateDispatch(createShiftParticipationAction(sp)) }}
            />
            <pre>{JSON.stringify(pageState, null, 2)}</pre>
        </>
    );
}
export default ShiftsEditApp;