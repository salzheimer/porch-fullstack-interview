import axios from 'axios';

const apiClient = axios.create({
  // Replace 8080 with your external Docker port from 'docker ps'
  baseURL: 'http://localhost:5000', 
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json'
  }
});

export default apiClient;
