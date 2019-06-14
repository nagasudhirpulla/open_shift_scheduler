/*
https://github.com/SophieDeBenedetto/catbook-redux/blob/blog-post/src/index.js

Using flexbox for aligning items
https://www.youtube.com/watch?v=k32voqQhODc
https://codepen.io/anon/pen/VKxRoE?editors=1100
*/
import React, { Component } from 'react';
import { connect } from 'react-redux';
import './ShiftsTable.css';
import classNames from 'classnames';
import { updateShiftsUIData, updateShiftsInUI, updateShiftTypesInUI, updateEmployeesInUI, createShiftParticipation, createShiftParticipationFromGroup, removeShiftParticipation, removeShift, updateShiftComments } from '../actions/shiftsTableActions';
import essentialProps from '../reducers/essentialProps';
import deepmerge from 'deepmerge';
import { groupObjBy } from '../utils/objUtils'
import { dateToKeyString, dateToDisplayDate, dateToDayOfWeek } from '../utils/timeUtils';
import ShiftUICell from './ShiftUICell';
import PopPop from 'react-poppop';

class ShiftsTable extends Component {
    constructor(props) {
        super(props);
        this.loadShifts = this.loadShifts.bind(this);
        this.setModalShow = this.setModalShow.bind(this);
        this.setShiftGroupModalShow = this.setShiftGroupModalShow.bind(this);
        this.createShiftParticipation = this.createShiftParticipation.bind(this);
        this.createShiftParticipationFromGroup = this.createShiftParticipationFromGroup.bind(this);
        this.removeShiftParticipation = this.removeShiftParticipation.bind(this);
        this.removeAllShiftParticipations = this.removeAllShiftParticipations.bind(this);
        this.editShiftComments = this.editShiftComments.bind(this);
        this.onEmpSelForPartClick = this.onEmpSelForPartClick.bind(this);
        this.onShiftGroupSelForPartClick = this.onShiftGroupSelForPartClick.bind(this);

        // Don't call this.setState() here!
        this.state = {};
        this.state.props = props;
        this.state.start_date = "2019-04-15";
        this.state.end_date = "2019-04-25";
        this.state.modalShow = false;
        this.state.modalShowShiftGroup = false;
        this.state.activeShift = null;
        this.startDateInput = React.createRef();
        this.endDateInput = React.createRef();
        this.employeesComboBox = React.createRef();
        this.shiftGroupsComboBox = React.createRef();
    }

    loadShifts = () => {
        const startDateStr = this.startDateInput.current.value;
        const endDateStr = this.endDateInput.current.value;
        const startDate = new Date(startDateStr + "T00:00:00");
        const endDate = new Date(endDateStr + "T00:00:00");
        let baseAddr = this.state.props.server_base_addr;
        if (baseAddr == undefined) {
            baseAddr = essentialProps.shifts_ui.server_base_addr;
        }
        //console.log(startDateStr);
        //console.log(endDateStr);
        //console.log(baseAddr);
        //update nested state properties in react - https://stackoverflow.com/questions/43040721/how-to-update-nested-state-properties-in-react
        this.setState({ start_date: startDateStr, end_date: endDateStr });
        //this.state.props.updateShiftTypesInUI(baseAddr);
        //this.state.props.updateEmployeesInUI(baseAddr);
        //this.state.props.updateShiftsInUI(baseAddr, startDate, endDate);
        this.state.props.updateShiftsUIData(baseAddr, startDate, endDate);
    }

    createShiftParticipation = (shift) => {
        this.setState({ activeShift: shift });
        this.setModalShow(true);
    }

    createShiftParticipationFromGroup = (shift) => {
        this.setState({ activeShift: shift });
        this.setShiftGroupModalShow(true);
    }

    onEmpSelForPartClick = () => {
        this.setModalShow(false);
        let baseAddr = this.state.props.server_base_addr;
        if (baseAddr == undefined) {
            baseAddr = essentialProps.shifts_ui.server_base_addr;
        }
        let employeeId = this.employeesComboBox.current.value;
        // console.log(`The selected employee Id for shift participation is ${employeeId}`);
        this.state.props.createShiftParticipation(baseAddr, employeeId, this.state.activeShift);
    }

    onShiftGroupSelForPartClick = () => {
        this.setShiftGroupModalShow(false);
        let baseAddr = this.state.props.server_base_addr;
        if (baseAddr == undefined) {
            baseAddr = essentialProps.shifts_ui.server_base_addr;
        }
        let shiftGroupId = this.shiftGroupsComboBox.current.value;
        // console.log(`The selected employee Id for shift participation is ${employeeId}`);
        this.state.props.createShiftParticipationFromGroup(baseAddr, shiftGroupId, this.state.activeShift);
    }

    setModalShow = modalShow => {
        this.setState({ modalShow });
    }

    setShiftGroupModalShow = modalShowShiftGroup => {
        this.setState({ modalShowShiftGroup });
    }

    removeShiftParticipation = (shiftParticipation) => {
        let baseAddr = this.state.props.server_base_addr;
        if (baseAddr == undefined) {
            baseAddr = essentialProps.shifts_ui.server_base_addr;
        }
        if (confirm('Are you sure you want to delete...')) {
            this.state.props.removeShiftParticipation(baseAddr, shiftParticipation);
        }
    }

    removeAllShiftParticipations = (shift) => {
        // we will remove the shift itself
        let baseAddr = this.state.props.server_base_addr;
        if (baseAddr == undefined) {
            baseAddr = essentialProps.shifts_ui.server_base_addr;
        }
        if (confirm('Are you sure you want to delete all in shift...')) {
            this.state.props.removeShift(baseAddr, shift);
        }
    }

    editShiftComments = (shift) => {
        let baseAddr = this.state.props.server_base_addr;
        if (baseAddr == undefined) {
            baseAddr = essentialProps.shifts_ui.server_base_addr;
        }
        var existingComments = (shift.comments == undefined || shift.comments == null) ? "" : shift.comments;
        var updatedComments = prompt("Please edit comments", existingComments);
        if (updatedComments != null) {
            this.state.props.updateShiftComments(baseAddr, { ...shift, "comments": updatedComments });
        }
    }

    componentWillReceiveProps(nextProps) {
        this.setState({ props: nextProps });
    }

    render() {
        let props = deepmerge(essentialProps.shifts_ui, this.state.props);
        const startDate = new Date(this.state.start_date + "T00:00:00");
        const endDate = new Date(this.state.end_date + "T00:00:00");
        const colSize = Math.floor((100 / (props.shift_types.length)));
        const groupedEmployees = groupObjBy(props.employees, 'employeeId');
        const { modalShow } = this.state;
        const { modalShowShiftGroup } = this.state;
        // group the shift objects by date and shift type
        let groupedShifts = groupObjBy(props.shifts, 'shiftDate');
        for (var dateStr in groupedShifts) {
            groupedShifts[dateStr] = groupObjBy(groupedShifts[dateStr], 'shiftTypeId');
        }

        // group the shift types by shiftTypeId
        let groupedShiftTypes = groupObjBy(props.shift_types, 'shiftTypeId');

        /*
         Create a base shift matrix w.r.t date and shift type
         We get shift types in the order of shift sequence from the server itself
        */
        const shiftMatrix = [];
        for (let dateIter = 0; dateIter <= (endDate - startDate) / 86400000; dateIter++) {
            // create row for each shift date
            const shiftDate = new Date(startDate.getTime() + 86400000 * dateIter)
            const dateShifts = [];
            const dateKeyStr = dateToKeyString(shiftDate);
            const dateShiftsObj = groupedShifts[dateKeyStr];

            // create a shift object for each shift type
            for (let shiftTypeIter = 0; shiftTypeIter < props.shift_types.length; shiftTypeIter++) {
                const shiftType = props.shift_types[shiftTypeIter];
                const shiftTypeId = shiftType.shiftTypeId;
                let shiftObj = deepmerge(essentialProps.shifts_ui_cell.shift, {
                    "shiftTypeId": shiftTypeId,
                    "shiftDate": dateKeyStr
                })
                if (dateShiftsObj != undefined) {
                    if (dateShiftsObj[shiftTypeId] != undefined) {
                        shiftObj = dateShiftsObj[shiftTypeId][0];
                    }
                }
                dateShifts.push(shiftObj);
            }
            shiftMatrix.push(dateShifts);
        }
        //console.log(shiftMatrix);

        //create Rows before render
        let shiftMatrixRows = [];
        for (let dateIter = 0; dateIter < shiftMatrix.length; dateIter++) {
            let dateShifts = shiftMatrix[dateIter];
            let shiftDate = new Date(dateShifts[0]['shiftDate']);
            const matrixRow =
                <div className={classNames('row')} key={'row_' + dateIter}>
                    <div className={classNames('shift_date_display', 'col-md-auto', 'col-sm-12')} key={'date_display_' + dateIter}>
                        <span className={classNames('h6')}>{dateToDisplayDate(shiftDate)}</span><br />
                        <span className={classNames('h6')}>{dateToDayOfWeek(shiftDate)}</span>
                    </div>
                    <div className={classNames('col')}>
                        <div className={classNames('row')}>
                            {
                                dateShifts.map((shiftObj, shiftInd) =>
                                    <ShiftUICell
                                        key={shiftObj.shiftDate + shiftInd}
                                        shift={shiftObj}
                                        shift_type={groupedShiftTypes[shiftObj.shiftTypeId][0]}
                                        col_size={colSize}
                                        employees_dict={groupedEmployees}
                                        createShiftParticipation={this.createShiftParticipation}
                                        createShiftParticipationFromGroup={this.createShiftParticipationFromGroup}
                                        removeShiftParticipation={this.removeShiftParticipation}
                                        removeAllShiftParticipations={this.removeAllShiftParticipations}
                                        editShiftComments={this.editShiftComments}
                                    />
                                )
                            }
                        </div>
                    </div>
                </div>;
            shiftMatrixRows.push(matrixRow);
        }
        const commentsList =
            <div className={classNames('row')}>
                <table className={classNames('table', 'table-bordered', 'table-striped', 'table-hover')}>
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Shift</th>
                            <th>Comments</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            props.shifts.map((shiftObj, shiftInd) =>
                                (shiftObj.comments != null && shiftObj.comments != "") ? (<tr key={"table_comment" + shiftInd} >
                                    <td>{`${dateToDisplayDate(new Date(shiftObj.shiftDate))}, ${dateToDayOfWeek(new Date(shiftObj.shiftDate))}`}</td>
                                    <td>{groupedShiftTypes[shiftObj.shiftTypeId][0].name}</td>
                                    <td>{shiftObj.comments}</td>
                                    <td><button className={classNames('btn', 'btn-outline', 'btn-sm', 'shift_comm_btn')} onClick={() => this.editShiftComments(shiftObj)}><i class={"fa fa-commenting-o"}></i></button></td>
                                </tr>) : ""
                            )
                        }
                    </tbody>
                </table>
            </div>;

        return (
            <div className={classNames({ 'shifts_ui_table': true })}>
                {/* <span>{JSON.stringify(groupedShifts)}</span> */}
                {/* <span>{JSON.stringify(onDashBoardFetchClick())}</span> */}
                <div className={classNames('row')}>
                    <div className={classNames('col-md-12', 'dashboard_bar', 'd-flex', 'flex-sm-row', 'flex-column', 'justify-content-center', 'child_hor_space')}>
                        <label className={classNames('h6')}>From Date - </label>
                        <input type='date' ref={this.startDateInput} />
                        <label className={classNames('h6')}>To Date - </label>
                        <input type='date' ref={this.endDateInput} />
                        <button className={classNames('btn', 'btn-outline-primary', 'btn-sm')} onClick={this.loadShifts}>Load Shifts</button>
                    </div>
                </div>
                {shiftMatrixRows}
                {commentsList}
                <PopPop position="centerCenter"
                    open={modalShow}
                    closeBtn={true}
                    closeOnEsc={true}
                    onClose={() => this.setModalShow(false)}
                    closeOnOverlay={true}>
                    <h3>Add Employee</h3>
                    <select ref={this.employeesComboBox}>
                        {
                            props.employees.map((empObj, empInd) =>
                                <option value={empObj.employeeId} key={`empSelOpt_${empInd}`}>{empObj.name}</option>
                            )
                        }
                    </select>
                    <button onClick={this.onEmpSelForPartClick}>Add Employee</button>
                </PopPop>

                <PopPop position="centerCenter"
                    open={modalShowShiftGroup}
                    closeBtn={true}
                    closeOnEsc={true}
                    onClose={() => this.setShiftGroupModalShow(false)}
                    closeOnOverlay={true}>
                    <h3>Add from Shift Group</h3>
                    <select ref={this.shiftGroupsComboBox}>
                        {
                            props.shift_groups.map((shiftGrpObj, grpInd) =>
                                <option value={shiftGrpObj.shiftGroupId} key={`shiftGrpSelOpt_${grpInd}`}>{shiftGrpObj.name}</option>
                            )
                        }
                    </select>
                    <button onClick={this.onShiftGroupSelForPartClick}>Add From Group</button>
                </PopPop>
            </div>

        );
    }
};

const mapStateToProps = (state) => {
    return state.shifts_ui;
};

const mapDispatchToProps = (dispatch) => {
    return {
        updateShiftTypesInUI: (baseAddr) => {
            dispatch(updateShiftTypesInUI(baseAddr));
        },
        updateEmployeesInUI: (baseAddr) => {
            dispatch(updateEmployeesInUI(baseAddr));
        },
        updateShiftsInUI: (baseAddr, start_date, end_date) => {
            dispatch(updateShiftsInUI(baseAddr, start_date, end_date));
        },
        updateShiftsUIData: (baseAddr, start_date, end_date) => {
            dispatch(updateShiftsUIData(baseAddr, start_date, end_date));
        },
        createShiftParticipation: (baseAddr, employeeId, shift) => {
            dispatch(createShiftParticipation(baseAddr, employeeId, shift));
        },
        removeShiftParticipation: (baseAddr, shiftParticipation) => {
            dispatch(removeShiftParticipation(baseAddr, shiftParticipation));
        },
        createShiftParticipationFromGroup: (baseAddr, shiftGroupId, shift) => {
            dispatch(createShiftParticipationFromGroup(baseAddr, shiftGroupId, shift));
        },
        removeShift: (baseAddr, shift) => {
            dispatch(removeShift(baseAddr, shift));
        },
        updateShiftComments: (baseAddr, shift) => {
            dispatch(updateShiftComments(baseAddr, shift));
        }
    };
};

export default (
    connect(mapStateToProps, mapDispatchToProps)(ShiftsTable)
);
