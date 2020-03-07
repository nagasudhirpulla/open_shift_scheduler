import React, { useState } from "react";
import Modal from 'react-bootstrap/Modal'
import Select from 'react-select'
import { IAddFromShiftGroupModalProps } from "../type_defs/IAddFromShiftGroupModalProps";
import { IShiftGroup } from "../type_defs/IShiftGroup";

function AddFromShiftGroupModal(props: IAddFromShiftGroupModalProps) {
    const handleClose = () => props.setShow(false);
    const handleSaveChanges = () => {
        props.onShiftGroupSubmit(props.shift, selGrp.id);
        props.setShow(false);
    }

    const [selGrp, setSelGrp] = useState(props.shiftGroups.length > 0 ? props.shiftGroups[0] : null);
    const handleGrpChange = (selectedOption: IShiftGroup) => {
        setSelGrp(selectedOption)
    }

    return <Modal show={props.show} onHide={handleClose}>
        <Modal.Header closeButton>
            <Modal.Title>Add Employees From Shift Group</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <Select
                placeholder="Select Shift Group..."
                options={props.shiftGroups}
                onChange={handleGrpChange}
                getOptionLabel={option => option.name}
                getOptionValue={option => `${option.id}`} />
        </Modal.Body>
        <Modal.Footer>
            <button onClick={handleClose}>Close</button>
            <button onClick={handleSaveChanges}>Proceed</button>
        </Modal.Footer>
    </Modal>;
};

export default AddFromShiftGroupModal;