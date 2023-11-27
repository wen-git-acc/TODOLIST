import React, { useState } from 'react';
import styled from 'styled-components';

//const FormInput = styled.input`
//    width: 235px;
//    outline: none;
//    font-size: 13px;
//    padding-top: 7px;
//    padding-bottom: 7px;
//    padding-left: 10px;
//`;

//export default function TodoForm() {
//    return (
//        <form>
//            <FormInput placeholder="Enter new todo" />
//        </form>
//    )
//}

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

export default function TodoForm() {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [status, setStatus] = useState('notstarted');
    const [dueDate, setDueDate] = useState('');
    const [id, setId] = useState(generateUniqueId());

    function generateUniqueId() {
        return Math.floor(Math.random() * 1000000).toString();
    }

    function handleSubmit(e) {
        e.preventDefault();
        const formData = { id, name, description, status, dueDate };
        console.log('Submitted:', formData);
        setId(generateUniqueId());
        setName('');
        setDescription('');
        setStatus('notstarted');
        setDueDate('');
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
        </form>
    );
}