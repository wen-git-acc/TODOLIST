import React, { useContext } from "react";
import styled from 'styled-components';
import  TodoList  from './TodoList';
import TodoForm from './TodoForm';
import Login from "./Login";
import { AuthContext } from '../context/AuthContext'; 
import TodoFilter from "./TodoFilter";


var Container = styled.div`
    width: 100%;
    min-width: 300px !important;
    display: flex;
    flex-direction: column;
    text-align: center;
    margin: auto;
    gap: 20px;
    font-family: Arial, Helvetica, sans-serif;
    font-size: 13px;
    justify-content:center;
    align-items: center;
    `

export function TodoLayout() {
    const {isLogin} = useContext(AuthContext);

    return (
   
            <Container>
                <TodoForm />
                <TodoFilter />
                <TodoList />
                {!isLogin && <Login />}
            </Container>
           
  
    );
        
  
}
