import React, { useState } from "react";
import Modal from 'react-bootstrap/Modal'
import Select from 'react-select'
import { IAddEmployeeModalProps } from "../type_defs/IAddEmployeeModalProps";
import { IEmployee } from "../type_defs/IEmployee";
import { IShiftParticipationType } from "../type_defs/IShiftParticipationType";
import { IShiftParticipation } from "../type_defs/IShiftParticipation";

function AddEmployeeModal(props: IAddEmployeeModalProps) {
    const handleClose = () => props.setShow(false);
    const handleSaveChanges = () => {
        const newShiftPart: IShiftParticipation = {
            id: null,
            employeeId: selEmp.userId,
            shiftId: props.shift != null ? props.shift.id : null,
            shift: props.shift,
            shiftParticipationTypeId: selPartType.id,
            participationSequence: 0
        }
        props.onParticipationSubmit(newShiftPart);
        props.setShow(false);
    }

    const [selEmp, setSelEmp] = useState(props.employees.length > 0 ? props.employees[0] : null);
    const handleEmpChange = (selectedOption: IEmployee) => {
        setSelEmp(selectedOption)
    }

    const [selPartType, setSelPartType] = useState(props.shiftParticipationTypes.length > 0 ? props.shiftParticipationTypes[0] : null);
    const handlePartTypeChange = (selectedOption: IShiftParticipationType) => {
        setSelPartType(selectedOption)
    }

    return <Modal show={props.show} onHide={handleClose}>
        <Modal.Header closeButton>
            <Modal.Title>New Shift Participation</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <Select
                placeholder="Select Employee..."
                options={props.employees}
                onChange={handleEmpChange}
                getOptionLabel={option => option.username}
                getOptionValue={option => option.userId} />

            <Select
                placeholder="Select ParticipationType..."
                options={props.shiftParticipationTypes}
                onChange={handlePartTypeChange}
                getOptionLabel={option => option.name}
                getOptionValue={option => `${option.id}`} />
        </Modal.Body>
        <Modal.Footer>
            <button onClick={handleClose}>Close</button>
            <button onClick={handleSaveChanges}>Proceed</button>
        </Modal.Footer>
    </Modal>;
};

export default AddEmployeeModal;