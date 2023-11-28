import React, { createContext, useState } from 'react';

const AuthContext = createContext({});

function AuthProvider({ children} ) {
    const [isLogin, setIsLogin] = useState(false);

    function updateLoginStatus(isLogin){
        setIsLogin(isLogin);
    }
  
    const contextValue = {
        isLogin,
        updateLoginStatus,
    };

    return (
        <AuthContext.Provider value={contextValue}>
            {children}
        </AuthContext.Provider>
    );
};

export { AuthProvider, AuthContext };