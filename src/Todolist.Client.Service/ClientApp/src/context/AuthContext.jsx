import React, { createContext, useState } from 'react';

const AuthContext = createContext({});

function AuthProvider({ children} ) {
    const [accessToken, setAccessToken] = useState("");
    const [isLogin, setIsLogin] = useState(false);

    function updateAccessToken(newAccessToken) {
        setAccessToken(newAccessToken);
    }

    function updateLoginStatus(isLogin){
        setIsLogin(isLogin);
    }
  
    const contextValue = {
        accessToken,
        updateAccessToken,
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