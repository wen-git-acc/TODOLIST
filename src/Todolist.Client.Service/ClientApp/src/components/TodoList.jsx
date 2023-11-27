import React, { useEffect, useState, useContext } from 'react';
import styled from 'styled-components';
import TodoItem from './TodoItem';
import axios from 'axios';
import { AuthContext } from '../context/AuthContext';
import { TaskItemContext } from '../context/TaskItemContext';
import { getTaskItemList } from '../Client/todoListClient';

const StyledUl = styled.ul`
padding: 0
`


export default function TodoList() {
    const { accessToken, updateAccessToken, isLogin, updateLoginStatus, } = useContext(AuthContext);
    const { taskItemList, setTaskItemList } = useContext(TaskItemContext);

    useEffect(() => {
        const fetchData = async () => {
            var taskItemsList = await getTaskItemList();
            setTaskItemList(taskItemsList);
            console.log(taskItemsList);
           
        }
        if (isLogin) {
            fetchData();
        }
    }, [isLogin]); 

    return <StyledUl>
        {accessToken != "" && <div>{accessToken}</div>}
        {taskItemList.length > 0 && taskItemList.map(data => {
            return <TodoItem
                uniqueId={data["uniqueId"]}
                name={data["name"]}
                description={data["description"]}
                dueDate={data["dueDate"]}
                status={data["status"]}
            />
        })}
    </StyledUl>

}