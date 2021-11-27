import axios, { AxiosError, AxiosResponse } from "axios";
import {Course} from '../models/course';
import {Event} from '../models/event';

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

axios.interceptors.request.use( config => {
    return config;
});

axios.interceptors.response.use( async response => {
    return response;    
}, (error: AxiosError)=>{

});

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T> (url:string)=>axios.get<T>(url).then(responseBody),
    post: <T> (url:string, body : {})=>axios.get<T>(url,body).then(responseBody),
    put: <T> (url:string, body : {})=>axios.get<T>(url,body).then(responseBody),
    del: <T> (url:string)=>axios.delete<T>(url).then(responseBody)
}

const Courses ={ 
    list : () => requests.get<Course[]>('/Courses'),    
}

const Events ={ 
    list : () => requests.get<Event[]>('/Events'),    
}

const agent = {
    Courses,
    Events,
}

export default agent;