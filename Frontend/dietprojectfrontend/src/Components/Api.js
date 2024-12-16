import axios from 'axios';
import Cookies from 'js-cookie';

// Create the axios instance
const api = axios.create({
  baseURL: 'http://localhost:5177/api', // Your API base URL
  withCredentials: true, // Ensures cookies are sent with requests
});

// Intercept requests to add the accessToken from cookies
api.interceptors.request.use(config => {
  const token = Cookies.get('accessToken'); // Use 'accessToken' from cookies
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, error => {
  return Promise.reject(error);
});

// Intercept responses to handle 401 errors and refresh the access token
api.interceptors.response.use(response => {
  return response;
}, async error => {
  const originalRequest = error.config;

  if (error.response.status === 401 && !originalRequest._retry) {
    originalRequest._retry = true; // Mark the request as retried to avoid infinite loop

    try {
      const refreshResponse = await axios.post('http://localhost:5177/refresh-token', {}, { withCredentials: true });
      const newAccessToken = refreshResponse.data.accessToken;

      // Store the new access token in cookies
      Cookies.set('accessToken', newAccessToken);

      // Update the Authorization header in the original request
      originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;

      // Retry the original request with the new token
      return api(originalRequest);
    } catch (refreshError) {
      // Handle refresh token failure (e.g., logout user)
      console.error("Failed to refresh token", refreshError);
      
      // Clear cookies and user session data
      Cookies.remove('accessToken');
      
      return Promise.reject(refreshError);
    }
  }

  return Promise.reject(error);
});

export default api;
