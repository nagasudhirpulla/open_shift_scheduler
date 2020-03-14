import { groupObjBy, convertShiftsToMatrix } from "../utils/objUtils";
import { IGroupedEmployee } from "../type_defs/IGroupedEmployee";
import { IGroupedShiftType } from "../type_defs/IGroupedShiftType";
import { IGroupedShiftParticipationType } from "../type_defs/IGroupedShiftParticipationType";
import { dateToDisplayDate, dateToDayOfWeek } from "../utils/timeUtils";
import ShiftCell from "./ShiftCell";
import React from "react";
import { IShiftCellsMatrixProps } from "../type_defs/IShiftCellsMatrixProps";

function ShiftCellsMatrix(props: IShiftCellsMatrixProps) {
    const shiftMatrix = convertShiftsToMatrix(props.startDate, props.endDate, props.shifts, props.shiftTypes);
    // group the employees, shiftTypes, shiftParticipationTypes by Id
    const groupedEmployees = groupObjBy(props.employees, 'userId') as IGroupedEmployee;
    const groupedShiftTypes = groupObjBy(props.shiftTypes, 'id') as IGroupedShiftType;
    const groupedShiftPartTypes = groupObjBy(props.shiftParticipationTypes, 'id') as IGroupedShiftParticipationType;
    const colSize = Math.floor((12 / (props.shiftTypes.length)));
    //console.log(groupedShiftTypes)
    // create shiftmatrix Rows with cells that contain Shift Cells
    let shiftMatrixRows: JSX.Element[] = [];
    for (let dateIter = 0; dateIter < shiftMatrix.length && props.shiftTypes.length > 0; dateIter++) {
        let dateShifts = shiftMatrix[dateIter];
        let shiftDate = new Date(dateShifts[0]['shiftDate']);
        const matrixRow =
            <div className={'row ml-md-5 mr-md-5'} key={'row_' + dateIter}>
                <div className={['shift_date_display', 'col-md-auto', 'col-sm-12'].join(' ')} key={'date_display_' + dateIter}>
                    <span className={'h6'}>{dateToDisplayDate(shiftDate)}</span><br />
                    <span className={'h6'}>{dateToDayOfWeek(shiftDate)}</span>
                </div>
                <div className={'col'}>
                    <div className={'row'}>
                        {
                            dateShifts.map((shiftObj, shiftInd) =>
                                <ShiftCell
                                    key={`${shiftObj.shiftDate}_${shiftInd}`}
                                    colSize={colSize}
                                    groupedShiftParticipationTypes={groupedShiftPartTypes}
                                    groupedShiftTypes={groupedShiftTypes}
                                    groupedEmployees={groupedEmployees}
                                    shift={shiftObj}
                                    editShiftComments={props.editShiftComments}
                                    moveShiftParticipation={props.moveShiftParticipation}
                                    updateShiftParticipation={props.updateShiftParticipation}
                                    removeShiftParticipation={props.removeShiftParticipation}
                                    createShiftParticipation={props.createShiftParticipation}
                                    createShiftParticipationFromGroup={props.createShiftParticipationFromGroup}
                                    removeAllShiftParticipations={props.removeAllShiftParticipations}
                                />
                            )
                        }
                    </div>
                </div>
            </div>;
        shiftMatrixRows.push(matrixRow);
    }

    return (
        <>
            {shiftMatrixRows}
        </>
    );
}

export default ShiftCellsMatrix;