import React, { /*Component*/ } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import { AuthProvider } from './context/AuthContext'; 
import { TaskItemProvider } from './context/TaskItemContext';


function App() {

    return (
        <AuthProvider>
            <TaskItemProvider>
                <Layout>
                    <Routes>
                        {AppRoutes.map((route, index) => {
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                        })}
                    </Routes>    
                 </Layout>
            </TaskItemProvider>
        </AuthProvider>
    );
  }


export default App