import React, { useState } from "react";
import styled from 'styled-components';
import PropTypes from "prop-types";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
//const StyledList = styled.li`
//    list-style: none;
//    overflow: hidden;
//    width: 100%;
//    margin-bottom: 10px
//`
//const StyledLabel = styled.label`
//    float: left;
//    cursor: pointer
//`
//const StyledButton = styled.button`
//    float: right;
//    background: palevioletred;
//    color: #FFF;
//    border-radius: 3px;
//    border: 2px solid palevioletred;
//    padding: 3px 10px;
//    outline: none;
//    cursor: pointer
//`


//export default function TodoItem(props) {
//    const { id, title } = props

//    return (
//        <StyledList>
//            <StyledLabel htmlFor={id}>
//                <input type="checkbox" id={id} /> {title}
//            </StyledLabel>
//            <StyledButton type="button">Delete</StyledButton>
//        </StyledList>
//    )
//}

//TodoItem.propTypes = {
//    title: PropTypes.string.isRequired,
//    id: PropTypes.string.isRequired
//}

//const StyledList = styled.li`
//    list-style: none;
//    overflow: hidden;
//    width: 100%;
//    margin-bottom: 10px;
//    border: 1px solid #ccc;
//    padding: 10px;
//`

//const StyledLabel = styled.label`
//    display: flex;
//    align-items: center;
//`

//const StyledCheckbox = styled.input`
//    margin-right: 10px;
//`

//const StyledInfo = styled.div`
//    flex-grow: 1;
//`

//const StyledButton = styled.button`
//    background: palevioletred;
//    color: #FFF;
//    border-radius: 3px;
//    border: 2px solid palevioletred;
//    padding: 3px 10px;
//    outline: none;
//    cursor: pointer;
//    margin-right: 5px;
//`

//const StyledInput = styled.input`
//    margin-right: 5px;
//`
//const TopSectionDiv = styled.div`
//    display: flex;
//    flex-direction: row;
//`
//export default function TodoItem(props) {
//    var { uniqueId, name, description, dueDate, status } = props;

//    uniqueId = "123";
//    name = 'ben';
//    description = "This is my first text";

//    dueDate = Date.now();
//    status = "inprogress";

//    const handleDelete = () => {
//        // Placeholder function for delete logic
//        console.log(`Deleting task with ID: ${uniqueId}`);
//    };

//    const handleAddPeople = () => {
//        // Placeholder function for add people logic
//        console.log(`Adding people to task with ID: ${uniqueId}`);
//    };

//    return (
//        <StyledList>
//            <TopSectionDiv>
//            <StyledLabel htmlFor={uniqueId}>
//                {/*<StyledCheckbox type="checkbox" id={uniqueId} />*/}
//                <StyledInfo>
//                    <div>{name}</div>
//                    <div>{status}</div>
//                    <div>{description}</div>
//                </StyledInfo>
//                </StyledLabel>
//                <StyledButton type="button" onClick={handleDelete}>Delete</StyledButton>
//            </TopSectionDiv>
//            <StyledInput type="text" placeholder="Enter user ID" />
//            <StyledButton type="button" onClick={handleAddPeople}>Add People</StyledButton>

//        </StyledList>
//    );
//}

//TodoItem.propTypes = {
//    uniqueId: PropTypes.string.isRequired,
//    name: PropTypes.string.isRequired,
//    description: PropTypes.string.isRequired,
//    dueDate: PropTypes.string.isRequired,
//    status: PropTypes.string.isRequired,
//};



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

const StyledCheckbox = styled.input`
    margin-right: 10px;
`

const StyledInfo = styled.div`
    position:relative;
    margin:auto;
    display: flex;
    flex-direction: column;
    text-align:center;
    align-items:center;
`

const StyledButton = styled.button`
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

    const [editName, setName] = useState(name);
    const [editDescription, setDescription] = useState(description);
    const [editStatus, setStatus] = useState(status);
    const [editDueDate, setDueDate] = useState(dueDate);
    const [isEdit, setIsEdit] = useState(false);
    const [owner, setOwner] = useState("");

    const handleDelete = (e) => {
        e.preventDefault();
        // Placeholder function for delete logic call delete client
        console.log(`Deleting task with ID: ${uniqueId}`);
    };

    const handleEdit = (e) => {
        e.preventDefault();
        setIsEdit(true);
    };

    const handleCancel = (e) => {
        e.preventDefault();
        //setName(initialName);
        //setDescription(initialDescription);
        //setStatus(initialStatus);
        //setDueDate(initialDueDate);
        setIsEdit(false);
    };

    const handleAddOwner = (e) => {
        e.preventDefault();
        // Placeholder function for export logic
        setOwner("");
        console.log(`Exporting task with ID: ${uniqueId} to owner: ${owner}`);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        // Placeholder function for submit logic
        console.log(editDueDate);
        console.log(typeof (editDueDate));
        var newTaskItem = {
            UniqueId: uniqueId,
            Name: editName,
            Description: editDescription,
            DueDate: (new Date(editDueDate)).toISOString(),
            Status: editStatus
        }

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
                                value={owner}
                                onChange={(e) => setOwner(e.target.value)}
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


                              //<DatePicker
                            //    selected={new Date(editDueDate)}
                            //    onChange={(date) => setDueDate(date.toISOString())}
                            //    dateFormat="yyyy-MM-dd"
                            //    />