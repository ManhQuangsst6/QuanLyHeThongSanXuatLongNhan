import apiService from "../../Common/ServiceAPI/apiService"
const name = '/Category'
export const GetByID = (id) => {
    return apiService.get(`${name}/GetByID?id=${id}`)
}
export const GetListAll = () => {
    return apiService.get(`${name}/GetListAll`)
}
export const Post = (data) => {
    return apiService.post(`${name}/Post`, data)
}
export const Update = (data) => {
    return apiService.put(`${name}/Update`, data)
}
export const Remove = (id) => {
    return apiService.delete(`${name}/Remove?id=${id}`)
}

