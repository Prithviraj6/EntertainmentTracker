import axios from "axios";

const apiClient = axios.create({
    baseURL: "https://localhost:7205/api",
});

export default apiClient;