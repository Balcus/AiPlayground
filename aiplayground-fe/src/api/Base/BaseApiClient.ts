import axios from "axios";

export const defaultHeaders = {
    "Content-Type": "application/json",
};

export const AiPlaygroundApiClient = axios.create({
    baseURL: "http://localhost:5226/api/",
    headers: defaultHeaders,
});