import axios from "axios";
import React, { useState } from "react";

export const AppContext = React.createContext<any>({});

const useHttp = () => {
  const axiosInstance = axios.create({
    baseURL: "https://localhost:7033/"
  });
  const [loading, setLoading] = useState(false);
  let isRefreshing = false;
  let failedQueue: any[] = [];

  const processQueue = (error: any, token: string | null = null) => {
    failedQueue.forEach((prom: any) => {
      if (error) {
        prom.reject(error);
      } else {
        prom.resolve(token);
      }
    });
    failedQueue = [];
  };

  const refreshToken = async () => {
    try {
      console.log("Refresh Token api was called");
      const response = await axiosInstance.post(
        "https://localhost:7033/api/Auth/refresh-token",
        {
          accessToken: localStorage.getItem("token"),
          refreshToken: localStorage.getItem("refreshToken"),
        }
      );
      const { accessToken, refreshToken } = response.data;

      // Store the new tokens in local storage
      localStorage.setItem("token", accessToken);
      localStorage.setItem("refreshToken", refreshToken);

      return accessToken;
    } catch (error) {
      throw error;
    }
  };

  axiosInstance.interceptors.request.use((config) => {
    setLoading(true);

    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = "Bearer " + token;
    }

    // Print this before sending any HTTP request
    console.log("Request was sent");
    return config;
  });

  axiosInstance.interceptors.response.use(
    (config) => {
      setLoading(false);

      // Print this when receiving any HTTP response
      console.log("Response was received");
      return config;
    },
    async (error) => {
      const originalRequest = error.config;
      setLoading(false);

      if (error.response.status === 401 && !originalRequest._retry) {
        if (isRefreshing) {
          
          // Token refresh is already in progress, add the request to the queue
          try {
            const token = await new Promise((resolve, reject) => {
              failedQueue.push({ resolve, reject });
            });
            originalRequest.headers["Authorization"] = "Bearer " + token;
            return await axiosInstance(originalRequest);
          } catch (err) {
            return await Promise.reject(err);
          }
        }

        originalRequest._retry = true;
        isRefreshing = true;

        return new Promise((resolve, reject) => {
          refreshToken()
            .then((token) => {
              originalRequest.headers["Authorization"] = "Bearer " + token;
              processQueue(null, token);
              resolve(axiosInstance(originalRequest));
            })
            .catch((error) => {
              processQueue(error, null);
              reject(error);
            })
            .finally(() => {
              isRefreshing = false;
            });
        });
      }

      return Promise.reject(error);
    }
  );
  return {axiosInstance, loading}
};

export default useHttp;