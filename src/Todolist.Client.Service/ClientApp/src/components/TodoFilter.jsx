import React, { useState, useContext } from 'react';
import styled from 'styled-components';
import { TaskItemContext } from '../context/TaskItemContext';
import { AuthContext } from '../context/AuthContext';
import { createTaskItem, getTaskItemList, getTaskItemsByFilter } from '../Client/todoListClient';

const StyledForm = styled.form`
  position:relative;
  margin-bottom: 20px;
  display: flex;
  flex-direction: column;
  gap: 10px;
  width: 250px;
  justify-center:center;
  text-align:center;

`;

const StyledLabel = styled.label`
  display: flex;
  flex-direction: column;
`;

const FilterSection = styled.span`
  font-size: 14px;
  font-weight: bold;
  margin-bottom: 5px;
`;

const StyledButton = styled.button`
  background-color: palevioletred;
  border: 2px solid palevioletred;
  color: #FFF;
  padding: 10px;
  cursor: pointer;
`;

const StyledInput = styled.input`
  padding: 5px;
`;

export default function TodoFilter() {
    const { taskItemList, updateTaskItemList } = useContext(TaskItemContext);
    const { updateLoginStatus } = useContext(AuthContext);
    const [filterName, setFilterName] = useState('');
    const [filterStatus, setFilterStatus] = useState('all');
    const [filterDueDate, setFilterDueDate] = useState('');
    const [sortOrder, setSortOrder] = useState('descending'); 


    async function handleFilters (e) {
        e.preventDefault();

        var name = filterName !== "" ? filterName : null;
        var status = filterStatus !== "all" ? filterStatus : null;
        var dueDate = filterDueDate !== "" ? (new Date(filterDueDate)).toISOString() : null;

        var { taskItemsData, isUnauthorized } = await getTaskItemsByFilter(status, name, dueDate, sortOrder);
        console.log(taskItemsData);
        if (isUnauthorized) {
            updateLoginStatus(false);
        }

        updateTaskItemList(taskItemsData);

        // Filter tasks based on entered criteria
     
        //// Update the filtered task list
        //updateFilteredTaskItemList(filteredTasks);
    };

    //const handleFilters = () => {
    //    // Clear filters and show all tasks
    //    setFilterName('');
    //    setFilterStatus('all');
    //    setFilterDueDate('');

    //    // Update the filtered task list with the original task list
    //    //updateFilteredTaskItemList(taskItemList);
    //};

    const handleSorts = (e) => {
        var sortValue = e.target.value;
        console.log("hisort");
        setSortOrder(sortValue);
        var isAscending = sortOrder.toLowerCase() == "ascending";
        var currentTaskItemList = [...taskItemList];
        currentTaskItemList.sort((a, b) => {
            const dateA = new Date(a["dueDate"]);
            const dateB = new Date(b["dueDate"]);
            const comparison = isAscending ? dateB - dateA : dateA - dateB;
            return comparison;
        })
        updateTaskItemList(currentTaskItemList);
    };

    return (
        <StyledForm onSubmit={handleFilters}>
            <FilterSection>Filter Tasks:</FilterSection>
            <StyledLabel>
                Name:
                <input type="text" value={filterName} onChange={(e) => setFilterName(e.target.value)} />
            </StyledLabel>
            <StyledLabel>
                Status:
                <select value={filterStatus} onChange={(e) => setFilterStatus(e.target.value)}>
                    <option value="all">All</option>
                    <option value="inprogress">In Progress</option>
                    <option value="completed">Completed</option>
                    <option value="notstarted">Not Started</option>
                </select>
            </StyledLabel>
            <StyledInput
                type="date"
                placeholder="Due Date"
                value={filterDueDate}
                onChange={(e) => setFilterDueDate(e.target.value)}
            />
            <StyledLabel>
                Sort Order:
                <select value={sortOrder} onChange={handleSorts}>
                    <option value="ascending">Ascending</option>
                    <option value="descending">Descending</option>
                </select>
            </StyledLabel>
            <StyledButton type="submit">
                Filters
            </StyledButton>
        </StyledForm>
    );
}
