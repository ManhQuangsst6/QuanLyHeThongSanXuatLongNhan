import apiService from "../../Common/ServiceAPI/apiService"
const name = '/Employee'
export const GetByID = (id) => {
    return apiService.get(`${name}/GetByID?id=${id}`)
}
export const GetListEmployeePage = () => {
    return apiService.get(`${name}/GetListEmployeePage`)
}
export const GetAllUserPage = (paging) => {
    console.log(paging)
    let query = `${name}/GetAllUserPage?pageNum=${paging.pageNum}&pageSize=${paging.pageSize}`
    if (paging.nameSearch !== null && paging.nameSearch !== "")
        query += `&nameSearch=${paging.nameSearch}`
    return apiService.get(query)
}
export const Remove = (id) => {
    return apiService.delete(`${name}/Remove?id=${id}`)
}
