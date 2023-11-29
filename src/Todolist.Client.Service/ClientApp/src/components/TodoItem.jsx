import React, { useState, useContext } from "react";
import styled from 'styled-components';
import PropTypes from "prop-types";
import { updateTask, deleteTaskItem } from "../Client/todoListClient";
import { AuthContext } from '../context/AuthContext';
import { TaskItemContext } from '../context/TaskItemContext';

const StyledList = styled.li`
    list-style: none;
    width: 600px;
    minimum-width: 300px;
    margin-bottom: 10px;
    margin: auto;
    border: 1px solid #ccc;
    padding: 10px;
    position: relative;
    
`

const StyledLabel = styled.label`
    display: flex;
    align-items: center;
`

const StyledInfo = styled.div`
    position:relative;
    margin:auto;
    display: flex;
    flex-direction: column;
    text-align:center;
    align-items:center;
`

const StyledDeleteButton = styled.button`
    background: palevioletred;
    color: #FFF;
    border-radius: 3px;
    border: 2px solid palevioletred;
    padding: 3px 10px;
    outline: none;
    cursor: pointer;
    position: absolute;
    top: 5px;
    right: 5px;
`

const StyledEditButton = styled.button`
    background: palevioletred;
    color: #FFF;
    border-radius: 3px;
    border: 2px solid palevioletred;
    padding: 3px 10px;
    outline: none;
    cursor: pointer;
    position: absolute;
    top: 5px;
    left: 5px;
`

const StyledSubmitButton = styled.button`
    background: palevioletred;
    color: #FFF;
    border-radius: 3px;
    border: 2px solid palevioletred;
    padding: 3px 10px;
    outline: none;
    cursor: pointer;
    position: absolute;
    bottom: 5px;
    right: 5px;
`

const StyledAddOwnerButton = styled.button`
    background: palevioletred;
    color: #FFF;
    border-radius: 3px;
    border: 2px solid palevioletred;
    padding: 3px 10px;
    outline: none;
    cursor: pointer;
    position: absolute;
    bottom: 5px;
    right: 5px;
`

const StyledInput = styled.input`
   
    margin-right: 5px;
    border: ${({ isEdit }) => (isEdit ? '1px solid #ccc' : 'none')};
`

const StyledSelect = styled.select`
    margin-right: 5px;
    border: ${({ isEdit }) => (isEdit ? '1px solid #ccc' : 'none')};
`

export default function TodoItem(props) {
    //Todo change back to const
    const { uniqueId, name , description , dueDate , status  } = props;
    const { updateLoginStatus } = useContext(AuthContext);
    const { updateTaskItemList } = useContext(TaskItemContext);
    const [editName, setName] = useState(name);
    const [editDescription, setDescription] = useState(description);
    const [editStatus, setStatus] = useState(status);
    const [editDueDate, setDueDate] = useState(dueDate);
    const [isEdit, setIsEdit] = useState(false);
    const [newOwner, setNewOwner] = useState("");

    async function handleDelete (e) {
        e.preventDefault();

        var newTaskItem = {
            UniqueId: uniqueId,
            Name: name,
            Description: description,
            DueDate: (new Date(dueDate)).toISOString(),
            Status: status,
        }

        var { taskItemsData, isUnauthorized } = await deleteTaskItem(newTaskItem);

        if (isUnauthorized) {
            updateLoginStatus(false);
        }

        updateTaskItemList(taskItemsData);

        console.log(`Deleting task with ID: ${uniqueId}`);
    };

    function handleEdit (e) {
        e.preventDefault();
        setIsEdit(true);
    };

    function handleCancel(e) {
        e.preventDefault();
        setIsEdit(false);
    };

    async function handleAddOwner (e) {
        if (newOwner.trim() === "") {
            return;
        };
    
        var currentTaskItem = {
            UniqueId: uniqueId,
            Name: name,
            Description: description,
            DueDate: (new Date(dueDate)).toISOString(),
            Status: status,
        }

        var { taskItemsData, isUnauthorized } = await updateTask(currentTaskItem, newOwner);

        if (isUnauthorized) {
            updateLoginStatus(false);
        }

        updateTaskItemList(taskItemsData);

        setNewOwner("");
        console.log(`Exporting task with ID: ${uniqueId} to owner: ${newOwner}`);
    };

    async function handleSubmit (e){
        e.preventDefault();

        var newTaskItem = {
            UniqueId: uniqueId,
            Name: editName,
            Description: editDescription,
            DueDate: (new Date(editDueDate)).toISOString(),
            Status: editStatus
        }

        var { taskItemsData, isUnauthorized } = await updateTask(newTaskItem, "");

        if (isUnauthorized) {
            updateLoginStatus(false);
        }

        updateTaskItemList(taskItemsData);

        console.log(`Submitting changes for task with ID: ${uniqueId}`);
        console.log(`Name: ${editName}, Description: ${editDescription}, Status: ${editStatus}`);
        setIsEdit(false);
    };

    return (
        <StyledList>
            <StyledDeleteButton type="button" onClick={handleDelete}>Delete</StyledDeleteButton>
            <StyledLabel htmlFor={uniqueId}>
                <StyledInfo>
                    {isEdit ? (
                        <>
                            <StyledInput
                                type="text"
                                placeholder="Enter name"
                                value={editName}
                                onChange={(e) => setName(e.target.value)}
                      
                            />
                            <StyledInput
                                type="text"
                                placeholder="Enter description"
                                value={editDescription}
                                onChange={(e) => setDescription(e.target.value)}
                             
                            />
                            <StyledSelect
                                value={editStatus}
                                onChange={(e) => setStatus(e.target.value)}
                             
                            >
                                <option value="notstarted">Not Started</option>
                                <option value="inprogress">In Progress</option>
                                <option value="completed">Completed</option>
                            </StyledSelect>
                            <StyledInput
                                type="date"
                                placeholder={editDueDate.split('T')[0]}
                                value={editDueDate}
                                onChange={(e) => setDueDate(e.target.value)}
                            />
                        </>
                    ) : (
                        <>
                            <div>Name: {name}</div>
                            <div>Description: {description}</div>
                            <div>Status: {status}</div>
                            <div>Due Date: {dueDate.split('T')[0]}</div>
                            <StyledInput
                                type="text"
                                placeholder="Enter new owner"
                                value={newOwner}
                                onChange={(e) => setNewOwner(e.target.value)}
                            />
                        </>
                    )}
                </StyledInfo>
                {isEdit ? (
                    <>
                        <StyledEditButton type="button" onClick={handleCancel}>Cancel</StyledEditButton>
                        <StyledSubmitButton type="button" onClick={handleSubmit}>Submit</StyledSubmitButton>
                    </>
                ) : (
                    <>
                        <StyledEditButton type="button" onClick={handleEdit}>Edit</StyledEditButton>
                        <StyledAddOwnerButton type="button" onClick={handleAddOwner}>Add Owner</StyledAddOwnerButton>
                    </>

                )}

            </StyledLabel>
        </StyledList>
    );
}

TodoItem.propTypes = {
    uniqueId: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,
    dueDate: PropTypes.string.isRequired,
    status: PropTypes.string.isRequired,
};


                        