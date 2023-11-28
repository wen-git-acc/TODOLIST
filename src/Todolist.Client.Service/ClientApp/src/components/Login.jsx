import React, { useState, useContext } from 'react';
import styled from 'styled-components';
import { AuthContext } from '../context/AuthContext'; 
import { getToken } from '../Client/authClient';

const BackdropContainer = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.5); 
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000; 
`;

const LoginContainer = styled.div`
  max-width: 300px;
  margin: auto;
  padding: 20px;
  background-color: white;
  border: 1px solid #ccc;
  border-radius: 5px;
  margin-top: 50px;
`;

const LoginForm = styled.form`
  display: flex;
  flex-direction: column;
`;

const Input = styled.input`
  margin-bottom: 10px;
  padding: 10px;
`;

const SubmitButton = styled.button`
  background-color: palevioletred;
  border: 2px solid palevioletred;
  color: #FFF;
  padding: 10px;
  cursor: pointer;
`;

const LoginResult = styled.div`
  margin-top: 10px;
  color: ${({ issuccess}) => (issuccess == "true" ? 'green' : 'red')};
`;

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const { updateLoginStatus } = useContext(AuthContext);
    const [loginResult, setLoginResult] = useState({message:"", Success:"false"});

    async function handleLogin(e) {
            e.preventDefault();
            // Replace the URL with your actual authentication endpoint
            var { message, token, isSuccess } = await getToken(username, password);
          
            sessionStorage.setItem("accessToken", token);
            updateLoginStatus(isSuccess);            setLoginResult({message:message, Success: isSuccess ? "true" : "false"});
    };

    return (
        <BackdropContainer>
        <LoginContainer>
            <h2>Login</h2>
            <LoginForm onSubmit={handleLogin}>
                <Input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
                <Input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <SubmitButton type="submit">Login</SubmitButton>
            </LoginForm>
            <LoginResult issuccess={loginResult.Success}>
                {loginResult.message}
            </LoginResult>
            </LoginContainer>
        </BackdropContainer>
    );
};

export default Login;