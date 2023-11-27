import React, { useContext } from "react";
import styled from 'styled-components';
import  TodoList  from './TodoList';
import TodoForm from './TodoForm';
import Login from "./Login";
import { AuthProvider, AuthContext } from '../context/AuthContext'; 


var Container = styled.div`
    
    width: 500px;
    display: flex;
    flex-direction: column;
    text-align: center;
    margin: auto;
    gap: 50px;
    font-family: Arial, Helvetica, sans-serif;
    font-size: 13px
    `

export function TodoLayout() {
    const {isLogin} = useContext(AuthContext);
    console.log(`hi${isLogin}`);
    return (
        <>
            <Container>
                <TodoForm />
                <TodoList />
            </Container>
            {!isLogin && <Login />}
        </>
    );
        
  
}
