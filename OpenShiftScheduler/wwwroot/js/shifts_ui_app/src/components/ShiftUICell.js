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

        return (
            <div className={classNames('col-md-' + props.col_size, 'shift_cell')}>
                <span>This is shifts table cell</span>
                <br />
                <span>{JSON.stringify(props)}</span>
            </div>
        );
    }
};

export default ShiftUICell;
