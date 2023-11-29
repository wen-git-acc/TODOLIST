import axios from 'axios';
export async function getToken(username, password) {
    var message = "";
    var token = "";
    var isSuccess = false;
    try {
        const data = {
            Name: username,
            Password: password
        };

        const response = await axios.post('/api/users/authenticate', data);
        const deserialData = response.data;
        token = deserialData["token"]
        sessionStorage.setItem("accessToken", token);

        message = "Login Success!";
        isSuccess = true;
    } catch (error) {
        message = "Login failed. Invalid Creds";
        isSuccess = false;
        console.log(error.message);

    }

    return {
        message,
        token,
        isSuccess,
    }
}