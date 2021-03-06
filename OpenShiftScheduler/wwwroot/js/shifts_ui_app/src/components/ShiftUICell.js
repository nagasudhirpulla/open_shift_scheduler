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
        // derive the participation type object of normal shift type
        let normalShiftPartTypeObj = props.normal_shift_part_type;

        let getPartDecorationClasses = (participationobj) => {
            let classList = ['h6'];
            let shiftParticipationTypeObj = normalShiftPartTypeObj;
            if (participationobj.shiftParticipationTypeId != null) {
                shiftParticipationTypeObj = props.shift_part_types[participationobj.shiftParticipationTypeId][0];
            }

            if (shiftParticipationTypeObj != null) {
                if (shiftParticipationTypeObj.isAbsence == true) {
                    classList.push("absence_shift_part");
                }
                if (shiftParticipationTypeObj.isBold == true) {
                    classList.push("font-weight-bold");
                }
                //if (shiftParticipationTypeObj.name.toLowerCase() == "from general") {
                //    classList.push("general_shift_part");
                //}
            }
            return classList;
        }
        let getPartDecorationStyleObj = (participationobj) => {
            let styleObj = {};
            let shiftParticipationTypeObj = normalShiftPartTypeObj;
            if (participationobj.shiftParticipationTypeId != null) {
                shiftParticipationTypeObj = props.shift_part_types[participationobj.shiftParticipationTypeId][0];
            }

            if (shiftParticipationTypeObj != null) {
                styleObj['color'] = shiftParticipationTypeObj.colorString;
            }            
            return styleObj;
        }
        //console.log(props);
        return (
            <div className={classNames('shift_cell', 'col-md-auto', 'd-flex', 'align-items-stretch', 'flex-column')} style={{ backgroundColor: props.shift_type.colorString, border: '1px dashed #aaa' }}>
                <div className={classNames('d-flex', 'flex-row-reverse')}>
                    <h6 classNames={classNames('shift_cell_type_name', 'small')}>{props.shift_type.name}</h6>
                    <button className={classNames('btn', 'btn-outline', 'btn-sm', 'shift_comm_btn')} onClick={() => props.editShiftComments(props.shift)}><i class={(props.shift.comments != "" && props.shift.comments != null) ? "fa fa-commenting-o" : "fa fa-comment-o"}></i></button>
                </div>
                <div>
                    {props.shift.shiftParticipations != null &&
                        props.shift.shiftParticipations.sort((a, b) => (a.participationSequence > b.participationSequence) ? 1 : ((b.participationSequence > a.participationSequence) ? -1 : 0)).map((participationobj, ind) =>
                            <div key={'participation_display_' + ind} className={classNames('part_disp_row', 'm-1')}>
                                <button className={classNames('btn', 'btn-outline-info', 'btn-sm', 'part_up_btn')} onClick={() => props.moveShiftParticipation(participationobj, -1)}><i class="fa fa-arrow-circle-o-down"></i></button>
                                <span style={getPartDecorationStyleObj(participationobj)} className={classNames(...getPartDecorationClasses(participationobj))}>{(participationobj.participationSequence + 1) + ". "}{props.employees_dict[participationobj.employeeId][0]['name']}</span>
                                <button className={classNames('btn', 'btn-outline-info', 'btn-sm', 'part_down_btn')} onClick={() => props.moveShiftParticipation(participationobj, 1)}><i class="fa fa-arrow-circle-o-up"></i></button>
                                <button className={classNames('btn', 'btn-outline-warning', 'btn-sm', 'part_edit_btn')} onClick={() => props.updateShiftParticipation(participationobj)}><i class="fa fa-pencil"></i></button>
                                <button className={classNames('btn', 'btn-outline-danger', 'btn-sm', 'part_del_btn')} onClick={() => props.removeShiftParticipation(participationobj)}><i class="fa fa-trash-o"></i></button>
                            </div>
                        )
                    }
                </div>
                <div className={classNames('cell_btns_div', 'd-flex', 'flex-sm-row', 'flex-column', 'mt-auto')}>
                    <button onClick={() => props.createShiftParticipation(props.shift)} className={classNames('btn', 'btn-sm', 'btn-outline-success', 'part_add_btn', 'm-1')} ><span className={classNames('h6', 'small')}>+Employee</span></button>
                    <button onClick={() => props.createShiftParticipationFromGroup(props.shift)} className={classNames('btn', 'btn-sm', 'btn-outline-success', 'grp_add_btn', 'm-1')} ><span className={classNames('h6', 'small')}>+Shift Group</span></button>
                    <button onClick={() => props.removeAllShiftParticipations(props.shift)} className={classNames('btn', 'btn-sm', 'btn-outline-danger', 'rem_all_part_btn', 'm-1')} ><span className={classNames('h6', 'small')}>-Remove All</span></button>
                </div>
                {/* <div>{JSON.stringify(props)}</div> */}
            </div>
        );
    }
};

export default ShiftUICell;
