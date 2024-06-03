import apiService from "../../Common/ServiceAPI/apiService";
const name = "/Notifications";
export const GetCount = () => {
  return apiService.get(`${name}/GetCount`);
};
export const Read = (id) => {
  return apiService.get(`${name}/read?id=${id}`);
};

export const GetListByPage = (pageNum, pageSize) => {
  let query = `${name}/GetAll?pageNum=${pageNum}&pageSize=${pageSize}`;
  return apiService.get(query);
};
