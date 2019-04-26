/*
https://github.com/SophieDeBenedetto/catbook-redux/blob/blog-post/src/index.js

Using flexbox for aligning items
https://www.youtube.com/watch?v=k32voqQhODc
https://codepen.io/anon/pen/VKxRoE?editors=1100
*/
import React from 'react';
import { connect } from 'react-redux';
import DashboardCell from './DashboardCell';
import { Link } from "react-router-dom";
import './ShiftsTable.css';
import classNames from 'classnames';
import { processTransactions } from '../actions/shiftsTableActions'
import essentialProps from '../reducers/essentialProps'

class ShiftsTable extends React.Component {
    constructor(props) {
        super(props);
        this.processQueuedTransactions = this.processQueuedTransactions.bind(this);
        // Don't call this.setState() here!
        this.state = { };
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
        let props = deepmerge(essentialProps.dashboard, this.state.props);
        return (
            <div className={classNames('container-fluid', { 'dashboard': true })}>
                {/* <span>{JSON.stringify(dashboard)}</span> */}
                {/* <span>{JSON.stringify(onDashBoardFetchClick())}</span> */}
                <div className={classNames('row')}>
                    <div className={classNames('col-md-12', 'dashboard_bar')}>
                        <span>This is shifts table div</span>
                    </div>
                </div>
                <div className={classNames('row')}>
                <span>This is shifts table div 2</span>
                </div>
            </div>
        );
    }
};

const mapStateToProps = (state) => {
    return { shifts: state.shifts };
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
