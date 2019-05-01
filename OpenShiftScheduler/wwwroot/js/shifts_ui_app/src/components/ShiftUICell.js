/*
https://github.com/SophieDeBenedetto/catbook-redux/blob/blog-post/src/index.js

Using flexbox for aligning items
https://www.youtube.com/watch?v=k32voqQhODc
https://codepen.io/anon/pen/VKxRoE?editors=1100
*/
import React, { Component } from 'react';
import classNames from 'classnames';
import essentialProps from '../reducers/essentialProps';
import deepmerge from 'deepmerge';
import './ShiftUICell.css';

class ShiftUICell extends Component {
    constructor(props) {
        super(props);
        // Don't call this.setState() here!
        this.state = {};
        this.state.props = props;
    }

    componentWillReceiveProps(nextProps) {
        this.setState({ props: nextProps });
    }

    render() {
        let props = deepmerge(essentialProps.shifts_ui_cell, this.state.props);
        //console.log(props);
        return (
            <div className={classNames('col-md-' + props.col_size, 'shift_cell')} style={{ minHeight: '100px', backgroundColor: props.shift_type.colorString, border: '1px solid black' }}>
                <span className={classNames('shift_cell_type_name')}>{props.shift_type.name}</span>
                <div>
                    {
                        props.shift.shiftParticipations.map((participationobj, ind) =>
                            <div key={'participation_display_' + ind}><span>{props.employees_dict[participationobj.employeeId][0]['name']}</span></div>
                        )
                    }
                </div>
                <button onClick={() => props.createShiftParticipation(props.shift)}>Add Employee</button>
                {/* <div>{JSON.stringify(props)}</div> */}
            </div>
        );
    }
};

export default ShiftUICell;
