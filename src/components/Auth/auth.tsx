import axios from 'axios';

let token: any = null;

export const setConfig = () => {
    return {
        headers: { 'Authorization': `Bearer ${token}`}
    }
}

const login = async(credentials: any) => {
    const response = await axios.post('/api/Auth/login', credentials);
    return response.data;
}

const register = async(userData: any) => {
    const response = await axios.post('/api/Auth/register', userData);
    return response.data;
}

const authService = { login, register };

export default authService;