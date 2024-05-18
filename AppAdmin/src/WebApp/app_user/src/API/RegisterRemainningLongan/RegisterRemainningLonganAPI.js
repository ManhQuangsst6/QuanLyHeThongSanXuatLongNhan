import apiService from "../../Common/ServiceAPI/apiService"
const name = '/RegisterRemainningLongan'
export const GetByID = (id) => {
    return apiService.get(`${name}/GetByID?id=${id}`)
}

export const GetListByPage = (paging) => {
    let query = `${name}/GetListByPage?pageNum=${paging.pageNum}&pageSize=${paging.pageSize}
   `
    if (paging.nameSearch !== null && paging.nameSearch !== "")
        query += `&searchName=${paging.nameSearch}`
    if (paging.dateSearch !== null && paging.dateSearch !== "")
        query += `&dateTime=${paging.dateSearch}`
   
    return apiService.get(query)
}
export const Remove = (id) => {
    return apiService.delete(`${name}/Remove?id=${id}`)
}
export const Post = (data) => {
    return apiService.post(`${name}/Post`, data)
}
export const Update = (data) => {
    return apiService.put(`${name}/Update`, data)
}