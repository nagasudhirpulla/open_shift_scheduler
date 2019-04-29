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
import { updateShiftsInUI, updateShiftTypesInUI, createShiftParticipation } from '../actions/shiftsTableActions';
import essentialProps from '../reducers/essentialProps';
import deepmerge from 'deepmerge';
import { groupObjBy } from '../utils/objUtils'
import { dateToKeyString, dateToDisplayDate } from '../utils/timeUtils'
import ShiftUICell from './ShiftUICell'

class ShiftsTable extends Component {
    constructor(props) {
        super(props);
        this.loadShifts = this.loadShifts.bind(this);
        this.createShiftParticipation = this.createShiftParticipation.bind(this);
        // Don't call this.setState() here!
        this.state = {};
        this.state.props = props;
        this.state.start_date = "2019-04-15";
        this.state.end_date = "2019-04-25";
        this.startDateInput = React.createRef();
        this.endDateInput = React.createRef();
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
        this.state.props.updateShiftTypesInUI(baseAddr);
        this.state.props.updateShiftsInUI(baseAddr, startDate, endDate);
    }

    createShiftParticipation = (shiftId) => {
        let baseAddr = this.state.props.server_base_addr;
        if (baseAddr == undefined) {
            baseAddr = essentialProps.shifts_ui.server_base_addr;
        }
        let employeeId = +prompt("Please enter employee Id", "1");
        this.state.props.createShiftParticipation(baseAddr, employeeId, shiftId);
    }

    componentWillReceiveProps(nextProps) {
        this.setState({ props: nextProps });
    }

    render() {
        let props = deepmerge(essentialProps.shifts_ui, this.state.props);
        const startDate = new Date(this.state.start_date + "T00:00:00");
        const endDate = new Date(this.state.end_date + "T00:00:00");
        const colSize = Math.floor((12 / (props.shift_types.length + 1)));
        const groupedEmployees = groupObjBy(props.employees, 'employeeId');
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
                    <div className={classNames('shift_date_display')} key={'date_display_' + dateIter}>
                        <span>{dateToDisplayDate(shiftDate)}</span>
                    </div>
                    {
                        dateShifts.map((shiftObj, shiftInd) =>
                            <ShiftUICell
                                key={shiftObj.shiftDate + shiftInd}
                                shift={shiftObj}
                                shift_type={groupedShiftTypes[shiftObj.shiftTypeId][0]}
                                col_size={colSize}
                                employees_dict={groupedEmployees}
                                createShiftParticipation = {this.createShiftParticipation}
                            />
                        )
                    }
                </div>;
            shiftMatrixRows.push(matrixRow);
        }
        return (
            <div className={classNames('container-fluid', { 'shifts_ui_table': true })}>
                {/* <span>{JSON.stringify(groupedShifts)}</span> */}
                {/* <span>{JSON.stringify(onDashBoardFetchClick())}</span> */}
                <div className={classNames('row')}>
                    <div className={classNames('col-md-12', 'dashboard_bar')}>
                        <label>From Date - </label>
                        <input type='date' ref={this.startDateInput} />
                        <label>To Date - </label>
                        <input type='date' ref={this.endDateInput} />
                        <button onClick={this.loadShifts}>Load Shifts</button>
                    </div>
                </div>
                {shiftMatrixRows}
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
        updateShiftsInUI: (baseAddr, start_date, end_date) => {
            dispatch(updateShiftsInUI(baseAddr, start_date, end_date));
        },
        createShiftParticipation: (baseAddr, employeeId, shiftId) => {
            dispatch(createShiftParticipation(baseAddr, employeeId, shiftId));
        }

    };
};

export default (
    connect(mapStateToProps, mapDispatchToProps)(ShiftsTable)
);
