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
import { processTransactions } from '../actions/shiftsTableActions';
import essentialProps from '../reducers/essentialProps';
import deepmerge from 'deepmerge';
import { groupObjBy } from '../utils/objUtils'
import ShiftUICell from './ShiftUICell'

class ShiftsTable extends Component {
    constructor(props) {
        super(props);
        this.processQueuedTransactions = this.processQueuedTransactions.bind(this);
        // Don't call this.setState() here!
        this.state = {};
        this.state.props = props;
    }

    processQueuedTransactions = async () => {
        //alert(`Saving Dashboard as ${this.dashBoardNameInput.current.value} with override ${this.state.saveOverride}...`);
        // derive the dashboard json,strip off csv arrays and then store it

    }

    componentWillReceiveProps(nextProps) {
        this.setState({ props: nextProps });
    }

    render() {
        let props = deepmerge(essentialProps.shifts_ui, this.state.props);
        // group the shift objects by date and shift type
        let groupedShifts = groupObjBy(props.shifts, 'shiftDate');
        for (var dateStr in groupedShifts) {
            groupedShifts[dateStr] = groupObjBy(groupedShifts[dateStr], 'shiftTypeId');
        }
        //console.log(groupedShifts);

        // 
        return (
            <div className={classNames('container-fluid', { 'dashboard': true })}>
                <span>{JSON.stringify(groupedShifts)}</span>
                {/* <span>{JSON.stringify(onDashBoardFetchClick())}</span> */}
                <div className={classNames('row')}>
                    <div className={classNames('col-md-12', 'dashboard_bar')}>
                        <span>This is shifts table div</span>
                    </div>
                </div>
                <div className={classNames('row')}>
                    {
                        Object.keys(groupedShifts).map((shiftDate, ind) =>
                            Object.keys(groupedShifts[shiftDate]).map((shiftTypeId, keyIndex) =>
                                <ShiftUICell
                                    key={keyIndex}
                                    shift={groupedShifts[shiftDate][shiftTypeId]}
                                />
                            )
                        )
                    }
                </div>
            </div>
        );
    }
};

const mapStateToProps = (state) => {
    return state.shifts_ui;
};

const mapDispatchToProps = (dispatch) => {
    return {
        processTransactions: (transactionsList) => {
            dispatch(processTransactions(transactionsList));
        }
    };
};

export default (
    connect(mapStateToProps, mapDispatchToProps)(ShiftsTable)
);
