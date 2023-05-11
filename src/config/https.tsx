import axios from "axios";
import React from "react";
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';

export const axiosInstance = axios.create({});

export const AppContext = React.createContext<any>({});

axios.interceptors.request.use(config => {
	// Print this before sending any HTTP request

    const token = localStorage.getItem('token')
    if (token) {
      config.headers.Authorization = 'Bearer '+ token;
    }

	console.log('Request was sent');
    <Box sx={{ display: 'flex' }}>
      <CircularProgress />
    </Box>
	return config;
});

axios.interceptors.response.use(config => {
	// Print this when receiving any HTTP response
	console.log('Response was received');
	return config;
});