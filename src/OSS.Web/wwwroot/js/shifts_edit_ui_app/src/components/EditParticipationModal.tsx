import React, { useState, useEffect } from "react";
import Modal from 'react-bootstrap/Modal'
import Select from 'react-select'
import { IEmployee } from "../type_defs/IEmployee";
import { IShiftParticipationType } from "../type_defs/IShiftParticipationType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";
import { IEditParticipationModalProps } from "../type_defs/IEditParticipationModalProps";
import { groupObjBy } from "../utils/objUtils";
import { IGroupedEmployee } from "../type_defs/IGroupedEmployee";
import { IGroupedShiftParticipationType } from "../type_defs/IGroupedShiftParticipationType";

function EditParticipationModal(props: IEditParticipationModalProps) {
    if (props.participation == null) {
        return (<></>);
    }
    const handleClose = () => props.setShow(false);
    const handleSaveChanges = () => {
        const editedShiftPart: IShiftParticipation = {
            ...props.participation,
            employeeId: selEmp.userId,
            shiftParticipationTypeId: selPartType.id,
            participationSequence: selSeq - 1,
        }
        props.onParticipationSubmit(editedShiftPart);
        props.setShow(false);
    }

    //TODO get grouped stuff from props
    const groupedEmployees = groupObjBy(props.employees, 'userId') as IGroupedEmployee;
    const groupedShiftPartTypes = groupObjBy(props.shiftParticipationTypes, 'id') as IGroupedShiftParticipationType;

    const [selEmp, setSelEmp] = useState(groupedEmployees[props.participation.employeeId][0]);
    const handleEmpChange = (selectedOption: IEmployee) => {
        setSelEmp(selectedOption)
    }

    const [selPartType, setSelPartType] = useState(groupedShiftPartTypes[props.participation.shiftParticipationTypeId][0]);
    const handlePartTypeChange = (selectedOption: IShiftParticipationType) => {
        setSelPartType(selectedOption)
    }

    const [selSeq, setSelSeq] = useState(props.participation.participationSequence + 1)

    useEffect(() => {
        setSelEmp(groupedEmployees[props.participation.employeeId][0])
        setSelPartType(groupedShiftPartTypes[props.participation.shiftParticipationTypeId][0])
        setSelSeq(props.participation.participationSequence + 1)
    }, [props.participation.id]); // Only re-run the effect if props.participation.id changes

    return <Modal show={props.show} onHide={handleClose}>
        <Modal.Header closeButton>
            <Modal.Title>Edit Shift Participation</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <Select
                placeholder="Select Employee..."
                options={props.employees}
                onChange={handleEmpChange}
                getOptionLabel={option => option.displayName}
                value={selEmp}
                getOptionValue={option => option.userId} />

            <Select
                placeholder="Select ParticipationType..."
                options={props.shiftParticipationTypes}
                onChange={handlePartTypeChange}
                value={selPartType}
                getOptionLabel={option => option.name}
                getOptionValue={option => `${option.id}`} />

            <label>{" "}Sequence {" "}</label>
            <input type="number" value={selSeq} onChange={(e) => setSelSeq(+e.target.value)} />
        </Modal.Body>
        <Modal.Footer>
            <button onClick={handleClose}>Close</button>
            <button onClick={handleSaveChanges}>Proceed</button>
        </Modal.Footer>
    </Modal>;
};

export default EditParticipationModal;