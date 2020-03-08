import React, { useState, useEffect } from "react";
import Modal from 'react-bootstrap/Modal'
import { ICommentEditModalProps } from "../type_defs/ICommentEditModalProps";

function CommentEditModal(props: ICommentEditModalProps) {
    const [comm, setComm] = useState("")
    const handleClose = () => props.setShow(false);

    const handleSaveChanges = () => {
        props.onCommentSubmit(comm.trim() == "" ? null : comm, props.shift);
        props.setShow(false);
    }

    // Similar to componentDidMount and componentDidUpdate:
    useEffect(() => {
        setComm(props.comment == null ? "" : props.comment)
    }, [props.shift]);

    return <Modal show={props.show} onHide={handleClose}>
        <Modal.Header closeButton>
            <Modal.Title>Edit Shift Comments</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <input type="text" value={comm} onChange={(e) => setComm(e.target.value)} />
        </Modal.Body>
        <Modal.Footer>
            <button onClick={handleClose}>Close</button>
            <button onClick={handleSaveChanges}>Proceed</button>
        </Modal.Footer>
    </Modal>;
};

export default CommentEditModal;