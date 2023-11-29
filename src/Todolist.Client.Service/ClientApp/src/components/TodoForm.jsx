import React, { useState, useContext } from 'react';
import styled from 'styled-components';
import { TaskItemContext } from '../context/TaskItemContext';
import { AuthContext } from '../context/AuthContext';
import { createTaskItem } from '../Client/todoListClient';

const FormContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 10px;
  width: 300px;
  margin: auto;
`;

const StyledInput = styled.input`
  padding: 5px;
`;

const StyledButton = styled.button`
  padding: 8px;
  background-color: palevioletred;
  border: 2px solid palevioletred;
  color: #FFF;
  cursor: pointer;
`;

const SubmitResult = styled.div`
  margin-top: 10px;
  color: ${({ success }) => (success ? 'green' : 'red')};
`;

export default function TodoForm() {
    const { updateLoginStatus } = useContext(AuthContext);
    const { updateTaskItemList } = useContext(TaskItemContext);
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [status, setStatus] = useState('notstarted');
    const [dueDate, setDueDate] = useState('');
    const [uniqueId, setUniqueId] = useState(generateUniqueId());
    const [submitResult, setSubmitResult] = useState(true);

    function generateUniqueId() {
        return Math.floor(Math.random() * 1000000).toString();
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setUniqueId(generateUniqueId());

        if (uniqueId === "" || name === "" || dueDate === "") {
            setSubmitResult(false);
            return;
        }

        var newTaskItem = {
            UniqueId: uniqueId,
            Name: name,
            Description: description,
            DueDate: (new Date(dueDate)).toISOString(),
            Status: status
        }
  
        var { taskItemsData, isUnauthorized } = await createTaskItem(newTaskItem);

        if (isUnauthorized) {
            updateLoginStatus(false);
        }

        updateTaskItemList(taskItemsData);
     
        setName('');
        setDescription('');
        setStatus('notstarted');
        setDueDate('');
        setSubmitResult(true);
    }

    return (
        <form onSubmit={handleSubmit}>
            <FormContainer>
                <StyledInput
                    type="text"
                    placeholder="Name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <StyledInput
                    type="text"
                    placeholder="Description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
                <label>
                    Status:
                    <select value={status} onChange={(e) => setStatus(e.target.value)}>
                        <option value="notstarted">Not Started</option>
                        <option value="inprogress">In Progress</option>
                        <option value="completed">Completed</option>
                    </select>
                </label>
                <StyledInput
                    type="date"
                    placeholder="Due Date"
                    value={dueDate}
                    onChange={(e) => setDueDate(e.target.value)}
                />
                <StyledButton type="submit">Add Task</StyledButton>
            </FormContainer>
            {!submitResult && < SubmitResult success={submitResult}>
                Please Submit with all field filled!
            </SubmitResult>}
        </form>
    );
}