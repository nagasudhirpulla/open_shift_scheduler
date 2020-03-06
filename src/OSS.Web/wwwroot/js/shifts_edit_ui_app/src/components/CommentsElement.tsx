import React from "react";
import { dateToDisplayDate, dateToDayOfWeek } from "../utils/timeUtils";
import { groupObjBy } from "../utils/objUtils";
import { IGroupedShiftType } from "../type_defs/IGroupedShiftType";
import { ICommentsElementProps } from "../type_defs/ICommentsElementProps";
function CommentsElement(props: ICommentsElementProps) {
    const groupedShiftTypes = groupObjBy(props.shiftTypes, 'shiftTypeId') as IGroupedShiftType;
    const commentsList =
        <div className={'row'}>
            <table className={['table', 'table-bordered', 'table-striped', 'table-hover'].join(' ')}>
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
                                <td><button className={['btn', 'btn-outline', 'btn-sm', 'shift_comm_btn'].join(' ')} onClick={() => props.editShiftComments(shiftObj)}><i className={"fa fa-commenting-o"}></i></button></td>
                            </tr>) : ""
                        )
                    }
                </tbody>
            </table>
        </div>;
    return commentsList;
};

export default CommentsElement;