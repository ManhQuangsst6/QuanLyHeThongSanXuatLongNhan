import apiService from "../../Common/ServiceAPI/apiService"
const name = '/UserManager'
export const LoginUser = (data) => {
    console.log(data)
    return apiService.post(`${name}/login`, data)
}
export const RegisterUser = (data) => {
    return apiService.post(`${name}`, data)
}
