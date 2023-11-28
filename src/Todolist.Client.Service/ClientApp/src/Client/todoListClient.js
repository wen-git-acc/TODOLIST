import axios from 'axios';

const todoListUrl = "/todolist";
function getAccessToken() {
    return sessionStorage.getItem('accessToken');
};
export async function getTaskItemList() {
    const path = "/gettask";
    const url = `${todoListUrl}${path}`;

    const accessToken = getAccessToken();

    var taskItemsData = [];
    var isUnauthorized = false;
    if (!accessToken) {
        console.log('No accessToken found in session storage');
        return;
    }

    const headers = {
        'Authorization': `Bearer ${accessToken}`,
    };

    try {
        const response = await axios.get(url, {headers});
        const deserialData = response.data;
        taskItemsData = [...deserialData];
  
    } catch (error) {
        if (error.response.status === 401) {
            isUnauthorized = true;
        }
        console.log(error.message);
    }

    return { taskItemsData, isUnauthorized };
}

export async function getTaskItemsByFilter(status, name, dueDate, order) {
    const path = "/gettaskbyfilterorsort";
    var queryParameter = `?oder${order}`;
    if (status != null) {
        queryParameter += `&status=${status}`;
    }

    if (name != null) {
        queryParameter += `&name=${name}`;
    }

    if (dueDate != null) {
        queryParameter += `&dueDate=${dueDate}`;
    }

    const url = `${todoListUrl}${path}${queryParameter}`;
   
    const accessToken = getAccessToken();

    var taskItemsData = [];
    var isUnauthorized = false;
    if (!accessToken) {
        console.log('No accessToken found in session storage');
        return;
    }

    const headers = {
        'Authorization': `Bearer ${accessToken}`,
    };

    try {
        const response = await axios.get(url, { headers });
        const deserialData = response.data;
        taskItemsData = [...deserialData];
    } catch (error) {
        if (error.response.status === 401) {
            isUnauthorized = true;
        }
        console.log(error.message);
    }

    return { taskItemsData, isUnauthorized };
}

export async function updateTask(data, newOwner) {
    var queryParameter=""
    newOwner = newOwner.trim();
    if (newOwner !== "") {
        queryParameter = `?newitemuser=${newOwner}`;
    }
    const path = "/updatetask";
    const url = `${todoListUrl}${path}${queryParameter}`;
    console.log(url);
    console.log(data);

    const accessToken = getAccessToken();

    var taskItemsData = [];
    var isUnauthorized = false;
    if (!accessToken) {
        console.log('No accessToken found in session storage');
        return;
    }

    const headers = {
        'Authorization': `Bearer ${accessToken}`,
    };

    try {
        const response = await axios.put(url, data, { headers });
        const deserialData = response.data;
        taskItemsData = [...deserialData];
    } catch (error) {
        if (error.response.status === 401) {
            isUnauthorized = true;
        }
        console.log(error.message);
    }

    return { taskItemsData, isUnauthorized };
}

export async function createTaskItem(data) {
    const path = "/createtask";
    const url = `${todoListUrl}${path}`;

    const accessToken = getAccessToken();

    var taskItemsData = [];
    var isUnauthorized = false;
    if (!accessToken) {
        console.log('No accessToken found in session storage');
        return;
    }

    const headers = {
        'Authorization': `Bearer ${accessToken}`,
    };

    try {
        const response = await axios.post(url, data,{ headers });
        const deserialData = response.data;
        taskItemsData = [...deserialData];
        console.log(taskItemsData);
    } catch (error) {
        if (error.response.status === 401) {
            isUnauthorized = true;
        }
        console.log(error.message);
    }

    return { taskItemsData,isUnauthorized };
}


export async function deleteTaskItem(data) {
    const path = "/deletetask";
    const url = `${todoListUrl}${path}`;
    console.log(url);
    const accessToken = getAccessToken();

    var taskItemsData = [];
    var isUnauthorized = false;
    if (!accessToken) {
        console.log('No accessToken found in session storage');
        return;
    }

    const headers = {
        'Authorization': `Bearer ${accessToken}`,
    };

    try {
        const response = await axios.delete(url, {
            data: data,
            headers: headers,
        });
        const deserialData = response.data;
        taskItemsData = [...deserialData];
        console.log(taskItemsData);
    } catch (error) {
        if (error.response.status === 401) {
            isUnauthorized = true;
        }
        console.log(error.message);
    }

    return { taskItemsData, isUnauthorized };
}




