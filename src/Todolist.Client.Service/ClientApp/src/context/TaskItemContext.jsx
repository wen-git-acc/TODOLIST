import React, { createContext, useState } from 'react';

const TaskItemContext = createContext({});

function TaskItemProvider({ children }) {
 
    const [taskItemList, setTaskItemList] = useState([]);

    function updateTaskItemList(taskItemList) {
        setTaskItemList([...taskItemList]);
    }

    const contextValue = {
        taskItemList,
        updateTaskItemList,
    };

    return (
        <TaskItemContext.Provider value={contextValue}>
            {children}
        </TaskItemContext.Provider>
    );
};

export { TaskItemProvider, TaskItemContext };