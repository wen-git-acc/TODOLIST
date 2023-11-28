import React, { useEffect, useContext } from 'react';
import styled from 'styled-components';
import TodoItem from './TodoItem';
import { AuthContext } from '../context/AuthContext';
import { TaskItemContext } from '../context/TaskItemContext';
import { getTaskItemList } from '../Client/todoListClient';

const StyledUl = styled.ul`
padding: 0
`
export default function TodoList() {
    const { isLogin, updateLoginStatus } = useContext(AuthContext);
    const { taskItemList, updateTaskItemList } = useContext(TaskItemContext);
 
    useEffect(() => {
        const fetchData = async () => {
            var { taskItemsData, isUnauthorized } = await getTaskItemList();
           
            if (isUnauthorized) {
                updateLoginStatus(false);
            }

            updateTaskItemList(taskItemsData);
  
        }
        if (isLogin) {
            fetchData();
        }
    }, [isLogin]); 
    
    return <StyledUl>
        {taskItemList.length > 0 && taskItemList.map(data => {
            return <TodoItem
                key={data["uniqueId"]}
                uniqueId={data["uniqueId"]}
                name={data["name"]}
                description={data["description"]}
                dueDate={data["dueDate"]}
                status={data["status"]}
            />
        })}
    </StyledUl>

}