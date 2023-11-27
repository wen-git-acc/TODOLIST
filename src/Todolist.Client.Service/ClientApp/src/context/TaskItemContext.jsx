import React, { createContext, useState } from 'react';

const TaskItemContext = createContext({});

function TaskItemProvider({ children }) {
 
    const [taskItemList, setTaskItemList] = useState([]);

    function updateTaskItemList(taskItemList) {
        setTaskItemList([...taskItemList]);
    }

    const contextValue = {
        taskItemList,
        setTaskItemList,
    };

    return (
        <TaskItemContext.Provider value={contextValue}>
            {children}
        </TaskItemContext.Provider>
    );
};

export { TaskItemProvider, TaskItemContext };