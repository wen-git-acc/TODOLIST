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
        console.log(error.message);
    }

    return taskItemsData;
}

export async function updateTask (){
    const path = "/gettask";
    const url = `${todoListUrl}${path}`;

    const accessToken = getAccessToken();

    var taskItemsData = [];
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
        console.log(error.message);
    }

    return taskItemsData;
}

export async function createTaskItem() {
    const path = "/createtask";
    const url = `${todoListUrl}${path}`;

    const accessToken = getAccessToken();

    var taskItemsData = [];
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
        console.log(error.message);
    }

    return taskItemsData;
}