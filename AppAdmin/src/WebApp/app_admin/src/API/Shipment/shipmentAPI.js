import apiService from "../../Common/ServiceAPI/apiService"
const name = '/Shipment'
export const GetByID = (id) => {
    return apiService.get(`${name}/GetByID?id=${id}`)
}

export const GetListShipmentByPage = (paging) => {
    console.log(paging)
    let query = `${name}/GetListByPage?pageNum=${paging.pageNum}&pageSize=${paging.pageSize}`
    if (paging.nameSearch !== null && paging.nameSearch !== "")
        query += `&shipmentCode=${paging.nameSearch}`
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