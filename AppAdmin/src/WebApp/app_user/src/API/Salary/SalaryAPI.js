import apiService from "../../Common/ServiceAPI/apiService";
const name = "/Salary";
export const GetByID = (id) => {
  return apiService.get(`${name}/GetByID?id=${id}`);
};
export const GetAllClient = (paging) => {
  console.log(paging);
  let query = `${name}/GetAllClient?pageNum=${paging.pageNum}&pageSize=${paging.pageSize}`;
  if (paging.quarterYear !== null && paging.quarterYear !== "")
    query += `&quarterYear=${paging.quarterYear}`;
  if (paging.year !== null && paging.year !== "")
    query += `&year=${paging.year}`;
  return apiService.get(query);
};
export const Remove = (id) => {
  return apiService.delete(`${name}/Remove?id=${id}`);
};
export const Post = (data) => {
  return apiService.post(`${name}/Post`, data);
};
export const Update = (data) => {
  return apiService.put(`${name}/Update`, data);
};
export const GetAllExportExcel = (quarterYear, year) => {
  return apiService.get(
    `${name}/GetAllExportExcel?quarterYear=${quarterYear}&year=${year}`
  );
};
