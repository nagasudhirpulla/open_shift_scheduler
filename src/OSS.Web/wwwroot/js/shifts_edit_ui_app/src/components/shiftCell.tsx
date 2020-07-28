import React from 'react';
import { IShiftCellProps } from "../type_defs/IShiftCellProps";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IShiftType } from '../type_defs/IShiftType';

function ShiftCell(props: IShiftCellProps) {
    const getPartDecorationStyleObj = (participationobj: IShiftParticipation): {} => {
        let styleObj = {};
        const shiftParticipationTypeObj = props.groupedShiftParticipationTypes[participationobj.shiftParticipationTypeId][0];

        if (shiftParticipationTypeObj != null) {
            styleObj['color'] = shiftParticipationTypeObj.colorString;
        }
        return styleObj;
    }

    const getPartDecorationClasses = (participationobj: IShiftParticipation) => {
        let classList = ['h6'];
        const shiftParticipationTypeObj = props.groupedShiftParticipationTypes[participationobj.shiftParticipationTypeId][0];

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

    const getShiftType = (): IShiftType => {
        return props.groupedShiftTypes[props.shift.shiftTypeId][0];
    }

    const getEmployeeUsername = (empId: string): string => {
        return props.groupedEmployees[empId] == undefined ? "" : props.groupedEmployees[empId][0].displayName
    }

    return (
        <div className={`shift_cell col-md-${props.colSize} d-flex align-items-stretch flex-column`} style={{ backgroundColor: getShiftType().colorString, border: '1px dashed #aaa' }}>
            <div className={'d-flex flex-row-reverse'}>
                <h6 className={'shift_cell_type_name small'}>{getShiftType().name}</h6>
                <button className={'btn btn-outline btn-sm shift_comm_btn'} onClick={() => props.editShiftComments(props.shift)}><i className={(props.shift.comments != "" && props.shift.comments != null) ? "far fa-comment-dots" : "far fa-comment"}></i></button>
            </div>
            <div>
                {props.shift.shiftParticipations != null &&
                    props.shift.shiftParticipations.sort((a, b) => (a.participationSequence > b.participationSequence) ? 1 : ((b.participationSequence > a.participationSequence) ? -1 : 0)).map((participationobj, ind) =>
                        <div key={'participation_display_' + ind} className={'part_disp_row m-1'}>
                            <button className={'btn btn-outline-info btn-sm part_up_btn'} onClick={() => props.moveShiftParticipation(participationobj, -1)}><i className="fas fa-arrow-alt-circle-down"></i></button>
                            <span style={getPartDecorationStyleObj(participationobj)} className={getPartDecorationClasses(participationobj).join(' ')}>{(participationobj.participationSequence + 1) + ". "}{getEmployeeUsername(participationobj.employeeId)}</span>
                            <button className={'btn btn-outline-info btn-sm part_down_btn'} onClick={() => props.moveShiftParticipation(participationobj, 1)}><i className="fas fa-arrow-alt-circle-up"></i></button>
                            <button className={'btn btn-outline-warning btn-sm part_edit_btn'} onClick={() => props.updateShiftParticipation(participationobj)}><i className="fas fa-pencil-alt"></i></button>
                            <button className={'btn btn-outline-danger btn-sm part_del_btn'} onClick={() => props.removeShiftParticipation(participationobj)}><i className="fas fa-trash-alt"></i></button>
                        </div>
                    )
                }
            </div>
            <div className={'cell_btns_div d-flex flex-sm-row flex-column mt-auto'}>
                <button onClick={() => props.createShiftParticipation(props.shift)} className={'btn btn-sm btn-outline-success part_add_btn m-1'} ><span className={'h6 small'}>+Employee</span></button>
                <button onClick={() => props.createShiftParticipationFromGroup(props.shift)} className={'btn btn-sm btn-outline-success grp_add_btn m-1'} ><span className={'h6 small'}>+Shift Group</span></button>
                <button onClick={() => props.removeAllShiftParticipations(props.shift)} className={'btn btn-sm btn-outline-danger rem_all_part_btn m-1'} ><span className={'h6 small'}>-Remove All</span></button>
            </div>
            {/* <div>{JSON.stringify(props)}</div> */}
        </div>
    );
}

export default ShiftCell;