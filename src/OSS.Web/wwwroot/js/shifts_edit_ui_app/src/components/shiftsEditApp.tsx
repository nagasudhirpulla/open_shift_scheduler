// https://react-bootstrap.github.io/components/modal/
import React, { useState } from 'react';
import DateTime from 'react-datetime';
import pageInitState from '../initial_states/shiftEditUIInitState';
import { useShiftsEditUIReducer } from '../reducers/shiftsEditUIReducer';
import moment from 'moment';
import { setStartTimeAction } from '../actions/SetStartTimeAction';
import { setEndTimeAction } from '../actions/SetEndTimeAction';
import { getShiftsForUiAction } from '../actions/GetShiftsForUiAction';
import ShiftCellsMatrix from './ShiftCellsMatrix';
import CommentsElement from './CommentsElement';
import { IShift } from '../type_defs/IShift';
import { IShiftParticipation } from '../type_defs/IShiftParticipation';
import { moveShiftParticipationAction } from '../actions/MoveShiftParticipationAction';
import { deleteShiftParticipationAction } from '../actions/DeleteShiftParticipationAction';
import { deleteShiftAction } from '../actions/DeleteShiftAction';
import AddEmployeeModal from './AddEmployeeModal';
import { createShiftParticipationAction } from '../actions/CreateShiftParticipationAction';
import { setActiveShiftAction } from '../actions/SetActiveShiftAction';
import AddFromShiftGroupModal from './AddFromShiftGroupModal';
import { createShiftParticipationsFromGroupAction } from '../actions/CreateShiftParticipationsFromGroupAction';
import { setActiveShiftParticipationAction } from '../actions/SetActiveShiftParticipationAction';
import EditParticipationModal from './EditParticipationModal';
import { editShiftParticipationAction } from '../actions/EditShiftParticipationAction';
import CommentEditModal from './CommentEditModal';
import { editShiftCommentAction } from '../actions/EditShiftCommentAction';

function ShiftsEditApp() {
    let [pageState, pageStateDispatch] = useShiftsEditUIReducer(pageInitState);
    const [showAddEmpModal, setAddEmpModalShow] = useState(false);
    const [showAddFromGroupModal, setAddFromGroupModalShow] = useState(false);
    const [showEditParticipationModal, setEditParticipationModalShow] = useState(false);
    const [showCommentEditModal, setCommentEditModalShow] = useState(false);
    const [startDate, setStartDate] = useState(pageState.ui.startDate);
    const [endDate, setEndDate] = useState(pageState.ui.endDate);

    const onStartTimeChanged = (timeObj) => {
        if (timeObj instanceof moment) {
            let dateObj = moment(timeObj).toDate();
            setStartDate(dateObj)
        }
    }
    const onEndTimeChanged = (timeObj) => {
        if (timeObj instanceof moment) {
            let dateObj = moment(timeObj).toDate();
            setEndDate(dateObj)
        }
    }

    const onLoadBtnClicked = () => {
        pageStateDispatch(setStartTimeAction(startDate));
        pageStateDispatch(setEndTimeAction(endDate));
        pageStateDispatch(getShiftsForUiAction())
    }

    const onEditShiftCommentsClicked = (s: IShift) => {
        pageStateDispatch(setActiveShiftAction(s))
        setCommentEditModalShow(true)
    }

    return (
        <>
            <h3>Edit Shifts</h3>
            <div className={"datePickerDiv"}>
                <span>Start Time{" "}</span>
                <DateTime
                    value={startDate}
                    dateFormat={'DD-MM-YYYY'}
                    timeFormat={false}
                    onChange={onStartTimeChanged}
                    className={"timePicker"}
                />
            </div>
            <div style={{ marginLeft: "0.5em" }} className={"datePickerDiv"}>
                <span>End Time{"  "}</span>
                <DateTime
                    value={endDate}
                    dateFormat={'DD-MM-YYYY'}
                    timeFormat={false}
                    onChange={onEndTimeChanged}
                    className={"timePicker"}
                />
            </div>
            <button onClick={onLoadBtnClicked} className={"btn btn-success btn-sm btn-icon-split loadBtn"}>
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
                editShiftComments={(s: IShift) => { onEditShiftCommentsClicked(s) }}
                moveShiftParticipation={(sp: IShiftParticipation, dir: -1 | 1) => { pageStateDispatch(moveShiftParticipationAction(sp, dir)) }}
                updateShiftParticipation={(sp: IShiftParticipation) => { pageStateDispatch(setActiveShiftParticipationAction(sp)); setEditParticipationModalShow(true); }}
                removeShiftParticipation={(sp: IShiftParticipation) => { pageStateDispatch(deleteShiftParticipationAction(sp)) }}
                createShiftParticipation={(s: IShift) => { pageStateDispatch(setActiveShiftAction(s)); setAddEmpModalShow(true); }}
                createShiftParticipationFromGroup={(s: IShift) => { pageStateDispatch(setActiveShiftAction(s)); setAddFromGroupModalShow(true); }}
                removeAllShiftParticipations={(s: IShift) => { pageStateDispatch(deleteShiftAction(s)) }}
            />
            <br />
            <CommentsElement
                shifts={pageState.ui.shifts}
                shiftTypes={pageState.ui.shiftTypes}
                editShiftComments={(s: IShift) => { onEditShiftCommentsClicked(s) }}
            />
            <AddEmployeeModal
                show={showAddEmpModal}
                setShow={setAddEmpModalShow}
                shift={pageState.ui.activeShift}
                employees={pageState.ui.employees}
                shiftParticipationTypes={pageState.ui.shiftParticipationTypes}
                onParticipationSubmit={(sp: IShiftParticipation) => { pageStateDispatch(createShiftParticipationAction(sp)) }}
            />
            {pageState.ui.activeShift &&
                <AddFromShiftGroupModal
                    show={showAddFromGroupModal}
                    setShow={setAddFromGroupModalShow}
                    shift={pageState.ui.activeShift}
                    shiftGroups={pageState.ui.shiftGroups}
                    onShiftGroupSubmit={(shift: IShift, shiftGroupId: number) => { pageStateDispatch(createShiftParticipationsFromGroupAction(shift, shiftGroupId)) }}
                />}
            <EditParticipationModal
                show={showEditParticipationModal}
                setShow={setEditParticipationModalShow}
                participation={pageState.ui.activeShiftParticipation}
                employees={pageState.ui.employees}
                shiftParticipationTypes={pageState.ui.shiftParticipationTypes}
                onParticipationSubmit={(sp: IShiftParticipation) => { pageStateDispatch(editShiftParticipationAction(sp)) }}
            />
            {pageState.ui.activeShift &&
                <CommentEditModal
                    show={showCommentEditModal}
                    setShow={setCommentEditModalShow}
                    shift={pageState.ui.activeShift}
                    comment={pageState.ui.activeShift.comments}
                    onCommentSubmit={(comm: string, s: IShift) => { pageStateDispatch(editShiftCommentAction(s, comm)) }}
                />}
            {/*<pre>{JSON.stringify(pageState, null, 2)}</pre>*/}
        </>
    );
}
export default ShiftsEditApp;